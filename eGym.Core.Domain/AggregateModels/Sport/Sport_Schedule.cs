using eGym.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_Schedule), Schema = "conf")]
    public class Sport_Schedule : Entity
    {
        #region Db Columns
        [Key]
        public int SS_ID { get; set; }
        public int? SS_DayOfWeek { get; set; }
        public bool SS_Everyday { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan SS_FromTime { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan SS_ToTime { get; set; }
        public bool SS_AllowSelfRegistration { get; set; }
        public bool SS_RequireRegistration { get; set; }
        public int? SS_RegistrationCloseBeforeMinutes { get; set; }
        public int SS_SportID { get; set; }
        #endregion

        #region Virtuals
        public virtual Sport_Master Sport_Master { get; set; }
        #endregion

        #region Construnctors
        public Sport_Schedule()
        {

        }
        #endregion
    }
}
