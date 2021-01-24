using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Workout_Master), Schema = "dbo")]
    public class Workout_Master : Entity,  IAggregateRoot
    {
        [Key]
        public int Wrk_ID { get; set; }
        public string Wrk_Alias { get; set; }
        public string Wrk_Description { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public WorkoutTargetEnum[] Wrk_Target { get; set; }
        public string Wrk_Note { get; set; }
        public DateTime Wrk_StartDate { get; set; }
        public DateTime? Wrk_EndDate { get; set; }
        public int Wrk_SportID { get; set; }
        public int Wrk_AthleteID { get; set; }

        public virtual Sport_Master Sport_Master { get; set; }
        public Athlete_Master Athlete_Master { get; set; }
        public virtual ICollection<Workout_ExerciceXWorkout> Workout_ExerciceXWorkouts { get; set; } = new HashSet<Workout_ExerciceXWorkout>();

    }
}
