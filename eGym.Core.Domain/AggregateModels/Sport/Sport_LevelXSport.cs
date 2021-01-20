using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_LevelXSport), Schema = "conf")]
    public class Sport_LevelXSport : Entity
    {
        #region Db Columns
        [Key]
        public int LXS_ID { get; set; }
        public int LXS_SportID { get; set; }
        public int LXS_LevelID { get; set; }
        #endregion

        #region Virtuals
        public virtual Sport_Master Sport_Master { get; set; }
        public virtual Sport_Level Sport_Level { get; set; }
        #endregion

        #region Constructors
        public Sport_LevelXSport()
        {

        }
        #endregion
    }
}
