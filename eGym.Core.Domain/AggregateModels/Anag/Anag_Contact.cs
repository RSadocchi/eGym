using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Anag_Contact), Schema = "dbo")]
    public class Anag_Contact : Entity
    {
        #region Db Columns
        [Key]
        public int Cnt_ID { get; set; }
        [Required]
        [MaxLength(150)]
        public string Cnt_Value { get; set; }
        public bool Cnt_DefaultInType { get; set; }
        public string Cnt_Note { get; set; }
        public int Cnt_AnagID { get; set; }
        public short Cnt_TypeID { get; set; }
        #endregion

        #region Virtuals
        public virtual Anag_Master Anag_Master { get; set; }
        [NotMapped]
        public EN_ContactType EN_ContactType => EN_ContactType.FromID(this.Cnt_TypeID);
        #endregion

        #region Constructors
        public Anag_Contact()
        {

        }
        #endregion
    }

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
        public string Doc_Path { get; set; }
        public short Doc_TypeID { get; set; }
        public short? Doc_EmitterID { get; set; }
        public int Doc_AnagID { get; set; }
        #endregion

        #region Virtuals
        public virtual Anag_Master Anag_Master { get; set; }
        #endregion

        #region Constructor
        public Anag_Document()
        {

        }
        #endregion
    }
}
