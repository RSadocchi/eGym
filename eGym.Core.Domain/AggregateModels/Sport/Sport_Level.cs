using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_Level), Schema = "conf")]
    public class Sport_Level : Entity
    {
        #region Db Columns
        [Key]
        public int SL_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string SL_Name { get; set; }
        public string SL_Description { get; set; }
        public string SL_Note { get; set; }
        #endregion

        #region Virtuals
        public virtual ICollection<Sport_LevelLocalized> Sport_LevelLocalizeds { get; set; }
        public virtual ICollection<Sport_LevelXSport> Sport_LevelXSports { get; set; }
        public virtual ICollection<Athlete_LevelXAthlete> Athlete_LevelXAthletes { get; set; }
        #endregion

        #region Construnctors
        public Sport_Level()
        {
            Sport_LevelLocalizeds = new HashSet<Sport_LevelLocalized>();
            Sport_LevelXSports = new HashSet<Sport_LevelXSport>();
            Athlete_LevelXAthletes = new HashSet<Athlete_LevelXAthlete>();
        }
        #endregion
    }
}
