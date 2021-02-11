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
        //[Required]
        [MaxLength(100)]
        public string Ang_FirstName { get; set; }
        //[Required]
        [MaxLength(100)]
        public string Ang_LastName { get; set; }
        public string Ang_BusinessName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Ang_TaxCode { get; set; }
        [MaxLength(30)]
        public string Ang_VATNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Ang_BirthDate { get; set; }
        //[Required]
        [MaxLength(150)]
        public string Ang_BirthCity { get; set; }
        //[Required]
        [MaxLength(5)]
        public string Ang_BirthCountrySpec { get; set; }
        //[Required]
        [MaxLength(3)]
        public string Ang_BirthCountry { get; set; }
        //[Required]
        [MaxLength(3)]
        public string Ang_Citizenship { get; set; }
        public string Ang_Avatar { get; set; }
        public string Ang_Note { get; set; }
        public short Ang_GenderID { get; set; }
        public int? Ang_UserID { get; set; }

        public virtual Athlete_Master Athlete_Master { get; set; }
        public virtual Country BirthCountry { get; set; }
        public virtual Country Citizenship { get; set; }
        public virtual ICollection<Anag_Address> Anag_Addresses { get; set; } = new HashSet<Anag_Address>();
        public virtual ICollection<Anag_Contact> Anag_Contacts { get; set; } = new HashSet<Anag_Contact>();
        public virtual ICollection<Anag_MasterRole> Anag_MasterRoles { get; set; } = new HashSet<Anag_MasterRole>();
        public virtual ICollection<Anag_CorporateRole> Anag_CorporateRoles { get; set; } = new HashSet<Anag_CorporateRole>();
        public virtual ICollection<Anag_Document> Anag_Documents { get; set; } = new HashSet<Anag_Document>();

        [NotMapped]
        public EN_Gender EN_Gender => EN_Gender.FromID(this.Ang_GenderID);
        [NotMapped]
        public string CompleteName => !string.IsNullOrWhiteSpace(this.Ang_VATNo) && !string.IsNullOrWhiteSpace(this.Ang_BusinessName) ? this.Ang_BusinessName : this.Ang_FirstName + " " + this.Ang_LastName;
        [NotMapped]
        public string AbbreviationName => !string.IsNullOrWhiteSpace(this.Ang_VATNo) && !string.IsNullOrWhiteSpace(this.Ang_BusinessName) ? this.Ang_BusinessName : (this.Ang_FirstName.Substring(0, 1) + ". " + this.Ang_LastName);
    }
}
