using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_EventResultType), Schema = "conf")]
    public class Sport_EventResultType : Entity
    {
        #region Db Columns
        [Key]
        public int SERT_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string SERT_Name { get; set; }
        public string SERT_Description { get; set; }
        public string SERT_Note { get; set; }
        #endregion

        #region Virtuals

        #endregion

        #region Construnctors
        public Sport_EventResultType()
        {

        }
        #endregion

        /*
         * public static EN_ResultForTypes Decision = new EN_ResultForTypes(10, "Decisione unanime");
         * public static EN_ResultForTypes SplitDescision = new EN_ResultForTypes(20, "Split decision");
         * public static EN_ResultForTypes TKO = new EN_ResultForTypes(30, "TKO");
         * public static EN_ResultForTypes KO = new EN_ResultForTypes(30, "KO");
         * public static EN_ResultForTypes Submission = new EN_ResultForTypes(40, "Sottomissione");
         */
    }
}
