using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_LevelLocalized), Schema = "conf")]
    public class Sport_LevelLocalized : Entity
    {
        [Key]
        public int SLL_ID { get; set; }
        [Required]
        [MaxLength(5)]
        public string SLL_Culture { get; set; }
        [Required]
        [MaxLength(50)]
        public string SLL_Name { get; set; }
        public string SLL_Description { get; set; }
        public int? SLL_MinAge { get; set; }
        public int? SLL_MaxAge { get; set; }
        public int SLL_LevelID { get; set; }


        public virtual Sport_Level Sport_Level { get; set; }

        public Sport_LevelLocalized()
        {

        }
    }
}
