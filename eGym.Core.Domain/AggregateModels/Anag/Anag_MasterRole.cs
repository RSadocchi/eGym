using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_MasterRole), Schema = "dbo")]
    public class Anag_MasterRole : Entity
    {
        [Key]
        public int AngR_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime AngR_StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? AngR_EndDate { get; set; }
        public int AngR_AnagID { get; set; }
        public short AngR_RoleID { get; set; }

        public virtual Anag_Master Anag_Master { get; set; }
        [NotMapped]
        public EN_AnagMasterRole EN_AnagMasterRole => EN_AnagMasterRole.FromID(this.AngR_RoleID);
    }
}
