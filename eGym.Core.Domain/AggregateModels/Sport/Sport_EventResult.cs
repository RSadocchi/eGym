using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_EventResult), Schema = "conf")]
    public class Sport_EventResult : Entity
    {
        #region Db Columns
        [Key]
        public int SER_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string SER_Name { get; set; }
        public string SER_Description { get; set; }
        public int SER_ValueForRanking { get; set; }
        public string SER_Note { get; set; }
        #endregion

        #region Virtuals
        public virtual ICollection<Sport_EventResultLocalized> Sport_EventResultLocalizeds { get; set; }
        #endregion

        #region Construnctors
        public Sport_EventResult()
        {
            Sport_EventResultLocalizeds = new HashSet<Sport_EventResultLocalized>();
        }
        #endregion
    }
}
