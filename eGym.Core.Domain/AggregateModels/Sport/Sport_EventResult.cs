using eGym.Core.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_EventResult), Schema = "conf")]
    public class Sport_EventResult : Entity
    {
        #region Db Columns
        [Key]
        public int SER_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string SER_Name { get; set; }
        public string SER_Description { get; set; }
        public int SER_ValueForRanking { get; set; }
        public string SER_Note { get; set; }
        #endregion

        #region Virtuals
        
        #endregion

        #region Construnctors
        public Sport_EventResult()
        {
            
        }
        #endregion

        /*
         * public static EN_ResultTypes Lose = new EN_ResultTypes(10, "Sconfitta");
         * public static EN_ResultTypes Pair = new EN_ResultTypes(20, "Pareggio");
         * public static EN_ResultTypes Win = new EN_ResultTypes(30, "Vittoria");
         */
    }
}
