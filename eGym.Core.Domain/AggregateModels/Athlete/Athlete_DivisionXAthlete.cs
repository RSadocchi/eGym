using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Athlete_DivisionXAthlete), Schema = "ath")]
    public class Athlete_DivisionXAthlete : Entity
    {
        #region Db Columns
        [Key]
        public int DXA_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime DXA_FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DXA_ToDate { get; set; }
        public int DXA_DivisionID { get; set; }
        public int DXA_AthleteID { get; set; }
        #endregion

        #region Virtuals
        public virtual Athlete_Master Athlete_Master { get; set; }
        public virtual Sport_Division Sport_Division { get; set; }
        #endregion

        #region Construnctors
        public Athlete_DivisionXAthlete()
        {

        }
        #endregion
    }
}
