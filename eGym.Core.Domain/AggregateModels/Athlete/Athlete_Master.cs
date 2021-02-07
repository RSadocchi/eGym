using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Athlete_Master), Schema = "ath")]
    public class Athlete_Master : Entity, IAggregateRoot
    {
        [Key]
        public int Ath_ID { get; set; }
        public double? Ath_Weight { get; set; }
        public double? Ath_Ranking { get; set; }
        public string Ath_Note { get; set; }
        public int? Ath_DivisionID { get; set; }
        public int Ath_LevelID { get; set; }
        public int Ath_SportID { get; set; }
        public int Ath_AnagID { get; set; }

        public virtual Anag_Master Anag_Master { get; set; }
        public virtual Sport_Master Sport_Master { get; set; }

        public virtual ICollection<Athlete_WeightXAthlete> Athlete_WeightXAthletes { get; set; } = new HashSet<Athlete_WeightXAthlete>();
        public virtual ICollection<Athlete_DivisionXAthlete> Athlete_DivisionXAthletes { get; set; } = new HashSet<Athlete_DivisionXAthlete>();
        public virtual ICollection<Athlete_LevelXAthlete> Athlete_LevelXAthletes { get; set; } = new HashSet<Athlete_LevelXAthlete>();
        //public virtual ICollection<Workout_Master> Workout_Masters { get; set; } = new HashSet<Workout_Master>();
    }
}
