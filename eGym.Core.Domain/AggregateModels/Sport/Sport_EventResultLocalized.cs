using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_EventResultLocalized), Schema = "conf")]
    public class Sport_EventResultLocalized : Entity
    {
        [Key]
        public int SerL_ID { get; set; }
        [Required]
        [MaxLength(5)]
        public string SerL_Culture { get; set; }
        [Required]
        [MaxLength(50)]
        public string SerL_Name { get; set; }
        public string SerL_Description { get; set; }
        public int SerL_EventResultID { get; set; }


        public virtual Sport_EventResult Sport_EventResult { get; set; }


        public Sport_EventResultLocalized()
        {

        }
    }
}
