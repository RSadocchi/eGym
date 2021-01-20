using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Athlete_LevelXAthlete), Schema = "ath")]
    public class Athlete_LevelXAthlete : Entity
    {
        #region Db Columns
        [Key]
        public int LXA_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime LXA_FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LXA_ToDate { get; set; }
        public int LXA_LevelID { get; set; }
        public int LXA_AthleteID { get; set; }
        #endregion

        #region Virtuals
        public virtual Athlete_Master Athlete_Master { get; set; }
        public virtual Sport_Level Sport_Level { get; set; }
        #endregion

        #region Construnctors
        public Athlete_LevelXAthlete()
        {

        }
        #endregion
    }
}
