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
        [Key]
        public int Ang_ID { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        [MaxLength(100)]
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
        /// Id utente del profilo di accesso
        /// </summary>
        public int? Ang_UserID { get; set; }

        public virtual Athlete_Master Athlete_Master { get; set; }
        public virtual Country Country { get; set; }
        /// <summary>
        /// Indirizzi
        /// </summary>
        public virtual ICollection<Anag_Address> Anag_Addresses { get; set; } = new HashSet<Anag_Address>();
        /// <summary>
        /// Contatti
        /// </summary>
        public virtual ICollection<Anag_Contact> Anag_Contacts { get; set; } = new HashSet<Anag_Contact>();
        /// <summary>
        /// Ruoli anagrafica
        /// </summary>
        public virtual ICollection<Anag_MasterRole> Anag_MasterRoles { get; set; } = new HashSet<Anag_MasterRole>();
        /// <summary>
        /// Ruoli societari
        /// </summary>
        public virtual ICollection<Anag_CorporateRole> Anag_CorporateRoles { get; set; } = new HashSet<Anag_CorporateRole>();
        /// <summary>
        /// Documenti
        /// </summary>
        public virtual ICollection<Anag_Document> Anag_Documents { get; set; } = new HashSet<Anag_Document>();

        [NotMapped]
        public EN_Gender EN_Gender => EN_Gender.FromID(this.Ang_GenderID);

        [NotMapped]
        public string CompleteName => this.Ang_FirstName + " " + this.Ang_LastName;
    }
}
