using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(CMS_Master), Schema = "cms")]
    public class CMS_Master : Entity, IAggregateRoot
    {
        #region Db Columns
        [Key]
        public int CMS_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string CMS_GroupKey { get; set; }
        [Required]
        [MaxLength(100)]
        public string CMS_ContextKey { get; set; }
        public short CMS_EditorTypeID { get; set; }
        public bool CMS_Active { get; set; }
        #endregion

        #region Virtuals
        public virtual ICollection<CMS_Content> CMS_Contents { get; set; }
        public virtual ICollection<CMS_History> CMS_Histories { get; set; }
        [NotMapped]
        public EN_EditorType EN_EditorType => EN_EditorType.FromID(this.CMS_EditorTypeID);
        #endregion

        #region Constructors
        public CMS_Master()
        {
            CMS_Contents = new HashSet<CMS_Content>();
            CMS_Histories = new HashSet<CMS_History>();
        }
        #endregion
    }
}
