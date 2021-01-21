using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_EventResultType), Schema = "conf")]
    public class Sport_EventResultType : Entity
    {
        #region Db Columns
        [Key]
        public int SERT_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string SERT_Name { get; set; }
        public string SERT_Description { get; set; }
        public string SERT_Note { get; set; }
        #endregion

        #region Virtuals
        public virtual ICollection<Sport_EventResultTypeLocalized> Sport_EventResultTypeLocalizeds { get; set; }
        #endregion

        #region Construnctors
        public Sport_EventResultType()
        {
            Sport_EventResultTypeLocalizeds = new HashSet<Sport_EventResultTypeLocalized>();
        }
        #endregion
    }

    [Table(nameof(Sport_EventResultTypeLocalized), Schema = "conf")]
    public class Sport_EventResultTypeLocalized : Entity
    {
        [Key]
        public int SertL_ID { get; set; }
        [Required]
        [MaxLength(5)]
        public string SertL_Culture { get; set; }
        [Required]
        [MaxLength(50)]
        public string SertL_Name { get; set; }
        public string SertL_Description { get; set; }
        public int SertL_EventResultTypeID { get; set; }


        public virtual Sport_EventResultType Sport_EventResultType { get; set; }


        public Sport_EventResultTypeLocalized()
        {

        }
    }
}
