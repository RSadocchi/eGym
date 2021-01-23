using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Log
{
    [Table(nameof(Batch_Master), Schema = "dbo")]
    public class Batch_Master : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Creation { get; set; }
        [Column(TypeName = "char(1)")]
        public bool Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastExecutionDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NextExecutionScheduleDateTime { get; set; }
        [Column(TypeName = "time")]
        public TimeSpan? LastExecutionDurationTime { get; set; }
        [Column(TypeName = "time")]
        public TimeSpan? ShorterExecutionDurationTime { get; set; }
        [Column(TypeName = "time")]
        public TimeSpan? LongerExecutionDurationTime { get; set; }

        public virtual ICollection<Batch_LogXBatch> LogXBatches { get; set; } = new HashSet<Batch_LogXBatch>();
    }
}
