using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Log
{
    [Table(nameof(Batch_LogXBatch), Schema = "dbo")]
    public class Batch_LogXBatch : Entity
    {
        [Key]
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int LogId { get; set; }

        public virtual Batch_Master Batch { get; set; }
        public virtual Log_Master Log { get; set; }
    }
}
