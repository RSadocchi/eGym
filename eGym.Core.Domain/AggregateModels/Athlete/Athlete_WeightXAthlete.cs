using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Athlete_WeightXAthlete), Schema = "ath")]
    public class Athlete_WeightXAthlete : Entity
    {
        #region Db Columns
        [Key]
        public int WXA_ID { get; set; }
        public double WXA_Weight { get; set; }
        [Column(TypeName = "date")]
        public DateTime WXA_FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? WXA_ToDate { get; set; }
        public int WXA_AthleteID { get; set; }
        #endregion

        #region Virtuals
        public virtual Athlete_Master Athlete_Master { get; set; }
        #endregion

        #region Construnctors
        public Athlete_WeightXAthlete()
        {

        }
        #endregion
    }
}
