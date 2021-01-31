using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Todo_Master), Schema = "dbo")]
    public class Todo_Master : Entity, IAggregateRoot
    {
        [Key]
        public int TD_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime TD_CreationDate { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public TodoPriorityEnum TD_Priority { get; set; }
        public bool TD_Important { get; set; }
        [Column(TypeName = "nvarchar(25)")]
        public TodoDeadlineEnum TD_Deadline { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TD_DeadlineDate { get; set; }
        [Column(TypeName = "time")]
        public TimeSpan? TD_DeadlineTime { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string TD_Title { get; set; }
        [Column(TypeName = "ntext")]
        public string TD_Content { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string TD_Note { get; set; }
        public short TD_StatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TD_StatusDate { get; set; }

        [NotMapped]
        public EN_TodoStatus EN_TodoStatus => EN_TodoStatus.FromID(this.TD_StatusID);
    }
}
