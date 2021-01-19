using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_AddressRole), Schema = "dbo")]
    public partial class Anag_AddressRole : Entity
    {
        [Key]
        public int AdrR_ID { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime AdrR_CreationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AdrR_DeadlineDate { get; set; }

        public int AdrR_AddressID { get; set; }

        [Column(TypeName = "nchar(3)")]
        public string AdrR_RoleID { get; set; }

        public Anag_AddressRole()
        {

        }

        public virtual Anag_Address Anag_Address { get; set; }

        [NotMapped]
        public EN_AddressRole EN_AddressRole => EN_AddressRole.FromID(this.AdrR_RoleID);
    }
}
