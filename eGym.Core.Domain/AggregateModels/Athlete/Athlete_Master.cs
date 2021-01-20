using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Athlete_Master), Schema = "ath")]
    public class Athlete_Master : Entity, IAggregateRoot
    {
        #region Db Columns
        [Key]
        public int Ath_ID { get; set; }
        public double? Ath_Weight { get; set; }
        public double? Ath_Ranking { get; set; }
        public string Ath_Note { get; set; }
        public int? Ath_DivisionID { get; set; }
        public int Ath_LevelID { get; set; }
        public int Ath_SportID { get; set; }
        public int Ath_AnagID { get; set; }
        #endregion

        #region Virtuals
        public virtual Anag_Master Anag_Master { get; set; }
        public virtual Sport_Master Sport_Master { get; set; }

        public virtual ICollection<Athlete_WeightXAthlete> Athlete_WeightXAthletes { get; set; }
        public virtual ICollection<Athlete_DivisionXAthlete> Athlete_DivisionXAthletes { get; set; }
        public virtual ICollection<Athlete_LevelXAthlete> Athlete_LevelXAthletes { get; set; }
        #endregion

        #region Constructors
        public Athlete_Master()
        {
            Athlete_WeightXAthletes = new HashSet<Athlete_WeightXAthlete>();
            Athlete_DivisionXAthletes = new HashSet<Athlete_DivisionXAthlete>();
            Athlete_LevelXAthletes = new HashSet<Athlete_LevelXAthlete>();
        }
        #endregion
    }
}
