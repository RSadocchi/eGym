using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(CMS_Content), Schema = "cms")]
    public class CMS_Content : Entity
    {
        #region Db Columns
        [Key]
        public int CMSC_ID { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string CMSC_Value { get; set; }
        [Required]
        [MaxLength(5)]
        public string CMSC_Culture { get; set; }
        public bool CMSC_Active { get; set; }
        public int CMSC_MasterID { get; set; }
        #endregion

        #region Virtuals
        public virtual CMS_Master CMS_Master { get; set; }
        #endregion

        #region Constructors
        public CMS_Content()
        {

        }
        #endregion
    }
}
