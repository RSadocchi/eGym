using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_AddressRole), Schema = "dbo")]
    public class Anag_AddressRole : Entity
    {
        #region Db Columns
        [Key]
        public int AdrR_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime AdrR_StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? AdrR_EndDate { get; set; }
        public int AdrR_AddressID { get; set; }
        public short AdrR_RoleID { get; set; }
        #endregion

        #region Virtuals
        public virtual Anag_Address Anag_Address { get; set; }
        [NotMapped]
        public EN_AddressRole EN_AddressRole => EN_AddressRole.FromID(this.AdrR_RoleID);
        #endregion

        #region Constructors
        public Anag_AddressRole()
        {

        }
        #endregion
    }
}
