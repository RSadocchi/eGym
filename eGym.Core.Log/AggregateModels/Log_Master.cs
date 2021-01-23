using eGym.Core.SeedWork;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace eGym.Core.Log
{
    [Table(nameof(Log_Master), Schema = "dbo")]
    public class Log_Master : Entity, IAggregateRoot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public LogLevel Level { get; set; }
        public DateTime DateTime { get; set; }
        [Column(TypeName = "ntext")]
        public string Message { get; set; }
        public int? ParentId { get; set; }
        public string CallerMemberName { get; set; }
        public int? CallerLineNumber { get; set; }
        public string CallerFilePath { get; set; }

        public virtual Log_Master Parent { get; set; }
        public virtual ICollection<Log_Master> Childs { get; set; } = new HashSet<Log_Master>();
        public virtual ICollection<Batch_LogXBatch> LogXBatches { get; set; } = new HashSet<Batch_LogXBatch>();

        public Log_Master() { }

        public Log_Master(
            LogLevel level,
            string message,
            int? parentId = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null,
            [CallerFilePath] string callerFilePath = null) : this()
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException($"Missinig Key Field '{nameof(Message)}'");

            this.Level = level;
            this.DateTime = DateTime.Now;
            this.Message = message;
            this.ParentId = parentId;
            this.CallerMemberName = callerMemberName;
            this.CallerLineNumber = callerLineNumber;
            this.CallerFilePath = callerFilePath;
        }

        public Log_Master(
            Exception exception,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null,
            [CallerFilePath] string callerFilePath = null) : this()
        {
            if (exception == null) throw new ArgumentNullException($"Missinig Key Field '{nameof(Message)}'");

            this.Level = LogLevel.Error;
            this.DateTime = DateTime.Now;
            this.Message = exception.ToString();
            this.CallerMemberName = callerMemberName;
            this.CallerLineNumber = callerLineNumber;
            this.CallerFilePath = callerFilePath;

            if (exception.InnerException != null)
                this.ElaborateInnerException(this, exception.InnerException);
        }

        /// <summary>
        /// Add childs recursively to Log_Master passed in arguments
        /// </summary>
        /// <param name="log">Parent where to add childs</param>
        /// <param name="exception">Data to creater child</param>
        /// <param name="counter">Recursion counter</param>
        private void ElaborateInnerException(Log_Master log, Exception exception, int counter = 0)
        {
            if (counter >= 100) throw new IndexOutOfRangeException($"Possible overflow in recursion at {nameof(Log_Master)}.{nameof(ElaborateInnerException)}, {nameof(counter)} was {counter}, limit is 100");
            if (exception == null) return;
            counter += 1;
            var child = new Log_Master()
            {
                Level = LogLevel.Error,
                DateTime = DateTime.Now,
                Message = exception.ToString(),
                ParentId = log.Id
            };
            if (exception.InnerException != null)
                ElaborateInnerException(child, exception.InnerException, counter);
            log.Childs.Add(child);
        }
    }
}
