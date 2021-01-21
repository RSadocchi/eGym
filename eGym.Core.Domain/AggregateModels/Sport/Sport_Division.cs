using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_Division), Schema = "conf")]
    public class Sport_Division : Entity
    {
        #region Db Columns
        [Key]
        public int SD_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string SD_Name { get; set; }
        public string SD_Description { get; set; }
        public string SD_Note { get; set; }
        #endregion

        #region Virtuals
        public virtual ICollection<Sport_DivisionLocalized> Sport_DivisionLocalizeds { get; set; }
        public virtual ICollection<Sport_DivisionXSport> Sport_DivisionXSports { get; set; }
        public virtual ICollection<Athlete_DivisionXAthlete> Athlete_DivisionXAthletes { get; set; }
        #endregion

        #region Construnctors
        public Sport_Division()
        {
            Sport_DivisionLocalizeds = new HashSet<Sport_DivisionLocalized>();
            Sport_DivisionXSports = new HashSet<Sport_DivisionXSport>();
            Athlete_DivisionXAthletes = new HashSet<Athlete_DivisionXAthlete>();
        }
        #endregion
    }
}
