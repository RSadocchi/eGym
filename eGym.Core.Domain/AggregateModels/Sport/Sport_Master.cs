using eGym.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_Master), Schema = "conf")]
    public class Sport_Master : Entity, IAggregateRoot
    {
        #region Db Columns
        [Key]
        public int Spr_ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Spr_Name { get; set; }
        [MaxLength(100)]
        public string Spr_FullName { get; set; }
        public string Spr_Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime Spr_FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Spr_ToDate { get; set; }
        public string Spr_Note { get; set; }
        #endregion

        #region Virtuals
        public virtual ICollection<Sport_DivisionXSport> Sport_DivisionXSports { get; set; }
        public virtual ICollection<Sport_LevelXSport> Sport_LevelXSports { get; set; }
        public virtual ICollection<Sport_Schedule> Sport_Schedules { get; set; }
        public virtual ICollection<Athlete_Master> Athlete_Masters { get; set; }
        #endregion

        #region Construnctors
        public Sport_Master()
        {
            Sport_DivisionXSports = new HashSet<Sport_DivisionXSport>();
            Sport_LevelXSports = new HashSet<Sport_LevelXSport>();
            Sport_Schedules = new HashSet<Sport_Schedule>();
            Athlete_Masters = new HashSet<Athlete_Master>();
        }
        #endregion
    }
}
