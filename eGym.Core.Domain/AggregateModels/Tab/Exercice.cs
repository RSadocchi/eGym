using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Exercice), Schema = "dbo")]
    public class Exercice : Entity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconCssClass { get; set; }
        public string ImagePath { get; set; }
        [Column(TypeName = "nvrchar(MAX)")]
        public string[] Media { get; set; }

        public virtual ICollection<Workout_ExerciceXWorkout> Workout_ExerciceXWorkouts { get; set; } = new HashSet<Workout_ExerciceXWorkout>();
    }
}
