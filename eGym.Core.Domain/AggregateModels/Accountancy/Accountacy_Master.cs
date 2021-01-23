using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Accountacy_Master))]
    public class Accountacy_Master : Entity, IAggregateRoot
    {
        [Key]
        public int Acc_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Acc_MovementDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Acc_ValueDate { get; set; }
        public int Acc_SignMultiplier { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal Acc_Amount { get; set; }
        [Column(TypeName = "decimal(7,4)")]
        public decimal? Acc_VATPerc { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal? Acc_VATAmount { get; set; }
        [Column(TypeName = "decimal(7,4)")]
        public decimal? Acc_RebatePerc { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal? Acc_RebateAmount { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal Acc_FullAmount { get; set; }
        public bool Acc_IsPartial { get; set; }
        public int? Acc_RefMovementID { get; set; }
        public bool Acc_IsFiscalMovement { get; set; }
        [MaxLength(350)]
        public string Acc_Description { get; set; }
        public short? Acc_DocTypeID { get; set; }
        [MaxLength(50)]
        public string Acc_DocNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Acc_IssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Acc_DeadlineDate { get; set; }
        public string Acc_DocPath { get; set; }
        [MaxLength(150)]
        public string Acc_DocNote { get; set; }
        public short Acc_ReasonID { get; set; }
        public short? Acc_TermID { get; set; }
        public short Acc_AnagID { get; set; }


        public Accountacy_Master()
        {

        }
    }
}
