using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(CMS_History), Schema = "cms")]
    public class CMS_History : Entity
    {
        #region Db Columns
        [Key]
        public int CMSH_ID { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string CMSH_Value { get; set; }
        [Required]
        [MaxLength(5)]
        public string CMSH_Culture { get; set; }
        public bool CMSH_Active { get; set; }
        public int CMSH_MasterID { get; set; }
        #endregion

        #region Virtuals
        public virtual CMS_Master CMS_Master { get; set; }
        #endregion

        #region Constructors
        public CMS_History()
        {

        }
        #endregion
    }
}
