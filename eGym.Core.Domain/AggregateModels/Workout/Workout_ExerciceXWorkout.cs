using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Workout_ExerciceXWorkout), Schema = "dbo")]
    public class Workout_ExerciceXWorkout : Entity
    {
        [Key]
        public int WExW_ID { get; set; }
        public int WExW_WorkoutID { get; set; }
        public int WExW_ExerciceID { get; set; }

        public int? WExW_WorkReps { get; set; }
        public TimeSpan? WExW_WorkTime { get; set; }
        public TimeSpan? WExW_WorkRest { get; set; }

        public int? WExW_SetReps { get; set; }
        public TimeSpan? WExW_SetTime { get; set; }
        public TimeSpan? WExW_SetRest { get; set; }
        
        public virtual Workout_Master Workout_Master { get; set; }
        public virtual Exercice Exercice { get; set; }
    }
}
