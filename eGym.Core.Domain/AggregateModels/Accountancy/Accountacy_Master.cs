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
        public int? Acc_AnagID { get; set; }

        public virtual Anag_Master Anag_Master { get; set; }
        public virtual Accountacy_Master Accountacy_Parent { get; set; }
        public virtual ICollection<Accountacy_Master> Accountacy_Childs { get; set; }
        [NotMapped]
        public virtual EN_AccountancyDocType EN_AccountancyDocType => this.Acc_DocTypeID.HasValue ? EN_AccountancyDocType.FromID(this.Acc_DocTypeID.Value) : null;
        [NotMapped]
        public virtual EN_PaymentReason EN_PaymentReason => EN_PaymentReason.FromID(this.Acc_ReasonID);
        [NotMapped]
        public virtual EN_PaymentTerm EN_PaymentTerm => this.Acc_TermID.HasValue ? EN_PaymentTerm.FromID(this.Acc_TermID.Value) : null;

        public Accountacy_Master()
        {
            Accountacy_Childs = new HashSet<Accountacy_Master>();
        }
    }
}
