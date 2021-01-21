using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Security.Identity
{
    public partial class ServiceToken
    {
        [Key]
        public int ST_ID { get; set; }
        
        [Required]
        [MaxLength(5)]
        public string ST_Code { get; set; }

        [Required]
        [MaxLength(10)]
        public string ST_RequestorCode { get; set; }

        [Required]
        public string ST_Token { get; set; }
        
        [Column(TypeName = "datetime")]
        public DateTime ST_IssueDateTime { get; set; }
    }
}
