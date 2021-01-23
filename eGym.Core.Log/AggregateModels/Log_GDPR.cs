using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Log
{

    [Table(nameof(Log_GDPR), Schema = "dbo")]
    public class Log_GDPR : Entity
    {
        [Key]
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserRole { get; set; }
        [Required]
        [MaxLength(50)]
        public GDPRActionEnum Action { get; set; }
        [Required]
        public string[] TableNames { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string[] FieldNames { get; set; }
        public string DbQuery { get; set; }
    }
}
