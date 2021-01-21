using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_DivisionLocalized), Schema = "conf")]
    public class Sport_DivisionLocalized : Entity
    {
        [Key]
        public int SDL_ID { get; set; }
        [Required]
        [MaxLength(5)]
        public string SDL_Culture { get; set; }
        [Required]
        [MaxLength(50)]
        public string SDL_Name { get; set; }
        public string SDL_Description { get; set; }
        public int? SDL_MinAge { get; set; }
        public int? SDL_MaxAge { get; set; }
        public double? SDL_MinWeight { get; set; }
        public double? SDL_MaxWeight { get; set; }
        public short? SDL_UnitOfMeasureID { get; set; }
        public int SDL_DivisionID { get; set; }


        public virtual Sport_Division Sport_Division { get; set; }
        [NotMapped]
        public EN_UnitOfMeasure EN_UnitOfMeasure => this.SDL_UnitOfMeasureID.HasValue ? EN_UnitOfMeasure.FromID(this.SDL_UnitOfMeasureID.Value) : null;


        public Sport_DivisionLocalized()
        {

        }
    }
}
