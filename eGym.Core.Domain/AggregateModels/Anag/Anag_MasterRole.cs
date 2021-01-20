using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_MasterRole), Schema = "dbo")]
    public class Anag_MasterRole : Entity
    {
        #region Db Columns
        [Key]
        public int AngR_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime AngR_StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? AngR_EndDate { get; set; }
        public int AngR_AnagID { get; set; }
        public short AngR_RoleID { get; set; }
        #endregion

        #region Virtuals
        public virtual Anag_Address Anag_Address { get; set; }
        [NotMapped]
        public EN_AddressRole EN_AddressRole => EN_AddressRole.FromID(this.AngR_RoleID);
        #endregion

        #region Constructors
        public Anag_MasterRole()
        {

        }
        #endregion
    }
}
