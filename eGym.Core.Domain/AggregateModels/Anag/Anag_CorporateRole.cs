using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_CorporateRole), Schema = "dbo")]
    public class Anag_CorporateRole : Entity
    {
        #region Db Columns
        [Key]
        public int CR_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime CR_StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CR_EndDate { get; set; }
        public int CR_AnagID { get; set; }
        public short CR_RoleID { get; set; }
        #endregion

        #region Virtuals
        public virtual Anag_Master Anag_Master { get; set; }
        [NotMapped]
        public EN_CorporateRole EN_CorporateRole => EN_CorporateRole.FromID(this.CR_RoleID);
        #endregion

        #region Constructors
        public Anag_CorporateRole()
        {

        }
        #endregion
    }
}
