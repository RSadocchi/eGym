using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_Address), Schema = "dbo")]
    public class Anag_Address : Entity
    {
        [Key]
        public int Adr_ID { get; set; }
        [Required]
        [MaxLength(250)]
        public string Adr_Address { get; set; }
        [MaxLength(250)]
        public string Adr_Address1 { get; set; }
        [MaxLength(250)]
        public string Adr_Address2 { get; set; }
        [MaxLength(10)]
        public string Adr_HouseNumber { get; set; }
        [MaxLength(10)]
        public string Adr_Staircase { get; set; }
        [MaxLength(10)]
        public string Adr_Interior { get; set; }
        [MaxLength(10)]
        public string Adr_Floor { get; set; }
        [Required]
        [MaxLength(150)]
        public string Adr_City { get; set; }
        [Required]
        [MaxLength(10)]
        public string Adr_PostalCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string Adr_CountrySpec { get; set; }
        [MaxLength(50)]
        public string Adr_District { get; set; }
        [Required]
        [MaxLength(3)]
        public string Adr_Country { get; set; }
        public int Adr_AnagID { get; set; }

        public virtual Anag_Master Anag_Master { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Anag_AddressRole> Anag_AddressRoles { get; set; } = new HashSet<Anag_AddressRole>();

    }
}
