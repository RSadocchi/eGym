using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Security.Identity
{
    [Table(nameof(Captcha), Schema = "Security")]
    public partial class Captcha
    {
        [Key] [MaxLength(32)] public string TokenID { get; set; }
        [Required] [MaxLength(10)] public string Answer { get; set; }
        public DateTime Validity { get; set; }
    }
}
