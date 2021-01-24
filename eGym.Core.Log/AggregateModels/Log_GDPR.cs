using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace eGym.Core.Log
{

    [Table(nameof(Log_GDPR), Schema = "dbo")]
    public class Log_GDPR : Entity
    {
        [Key]
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        [MaxLength(50)]
        public GDPRActionEnum Action { get; set; }
        [Required]
        public string User { get; set; }
        [MaxLength(50)]
        public string IPAddress { get; set; } //request?.HttpContext.Connection.RemoteIpAddress
        public string UserAgent { get; set; } //request?.Headers["User-Agent"].ToString()
        public string RequestUrl { get; set; }
        public string RequestCookies { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        //public string[] TableNames { get; set; }
        //[Column(TypeName = "ntext")]
        //public string[] FieldNames { get; set; }
        [MaxLength(50)]
        public HttpStatusCode ResponseStatusCode { get; set; }
        [Column(TypeName = "ntext")]
        public string ResponseBody { get; set; }
        [Column(TypeName = "time")]
        public TimeSpan? ExecutionTime { get; set; }
    }
}
