using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_Document), Schema = "dbo")]
    public class Anag_Document : Entity
    {
        #region Db Columns
        [Key]
        public int Doc_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Doc_CreationDate { get; set; }
        [MaxLength(250)]
        public string Doc_Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Doc_IssueDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Doc_ExpiringDate { get; set; }
        [MaxLength(100)]
        public string Doc_Number { get; set; }
        [Column(TypeName = "nchar(3)")]
        public string Doc_EmissionCountry { get; set; }
        [MaxLength(100)]
        public string Doc_EmissionCity { get; set; }
        [MaxLength(250)]
        public string Doc_EmissionNote { get; set; }
        [MaxLength(100)]
        public string Doc_EmitterName { get; set; }
        public string Doc_Path { get; set; }
        public short Doc_TypeID { get; set; }
        public short? Doc_EmitterID { get; set; }
        public int Doc_AnagID { get; set; }
        #endregion

        #region Virtuals
        public virtual Anag_Master Anag_Master { get; set; }
        [NotMapped]
        public EN_DocumentType EN_DocumentType => EN_DocumentType.FromID(this.Doc_TypeID);
        [NotMapped]
        public EN_DocumentEmitter EN_DocumentEmitter => this.Doc_EmitterID.HasValue ? EN_DocumentEmitter.FromID(this.Doc_EmitterID.Value) : null;
        #endregion

        #region Constructor
        public Anag_Document()
        {

        }
        #endregion
    }
}
