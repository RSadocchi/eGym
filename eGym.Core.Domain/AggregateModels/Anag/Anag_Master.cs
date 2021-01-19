using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_Master), Schema = "dbo")]
    public class Anag_Master : Entity, IAggregateRoot
    {
        #region Db Columns
        [Key]
        public int Ang_ID { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Ang_FirstName { get; set; }
        /// <summary>
        /// Cognome
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Ang_LastName { get; set; }
        /// <summary>
        /// Codice Fiscale
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Ang_TaxCode { get; set; }
        /// <summary>
        /// Partita IVA (es. per collaboratori esterni)
        /// </summary>
        [MaxLength(30)]
        public string Ang_VATNo { get; set; }
        /// <summary>
        /// Data di nascita
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime Ang_BirthDate { get; set; }
        /// <summary>
        /// Città di nascita
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Ang_BirthCity { get; set; }
        /// <summary>
        /// Provincia di nascita
        /// </summary>
        [Required]
        [MaxLength(5)]
        public string Ang_BirthCountrySpec { get; set; }
        /// <summary>
        /// Stato di nascita
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string Ang_BirthCountry { get; set; }
        /// <summary>
        /// Cittadinanza
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string Ang_Citizenship { get; set; }
        /// <summary>
        /// Avatar
        /// </summary>
        public string Ang_Avatar { get; set; }
        /// <summary>
        /// Note
        /// </summary>
        public string Ang_Note { get; set; }
        /// <summary>
        /// Sesso
        /// </summary>
        public short Ang_GenderID { get; set; }
        /// <summary>
        /// Stato civile
        /// </summary>
        public short? Ang_CivilStatusID { get; set; }
        #endregion

        #region Virtuals
        /// <summary>
        /// Indirizzi
        /// </summary>
        public virtual ICollection<Anag_Address> Anag_Addresses { get; set; }
        /// <summary>
        /// Contatti
        /// </summary>
        public virtual ICollection<Anag_Contact> Anag_Contacts { get; set; }
        [NotMapped]
        public EN_Gender EN_Gender => EN_Gender.FromID(this.Ang_GenderID);
        [NotMapped]
        public EN_CivilStatus EN_CivilStatus => this.Ang_CivilStatusID.GetValueOrDefault() > 0 ? EN_CivilStatus.FromID(this.Ang_CivilStatusID.Value) : null;
        #endregion

        #region Constructors
        public Anag_Master()
        {
            Anag_Addresses = new HashSet<Anag_Address>();
            Anag_Contacts = new HashSet<Anag_Contact>();
        }
        #endregion
    }
}
