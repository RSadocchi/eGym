using eGym.Core.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGym.Core.Domain
{
    [Table(nameof(Sport_Division), Schema = "conf")]
    public class Sport_Division : Entity
    {
        #region Db Columns
        [Key]
        public int SD_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string SD_Name { get; set; }
        public string SD_Description { get; set; }
        public int? SD_MinAge { get; set; }
        public int? SD_MaxAge { get; set; }
        public double? SD_MinWeight { get; set; }
        public double? SD_MaxWeight { get; set; }
        public string SD_Note { get; set; }
        #endregion

        #region Virtuals
        public virtual ICollection<Sport_DivisionXSport> Sport_DivisionXSports { get; set; }
        public virtual ICollection<Athlete_DivisionXAthlete> Athlete_DivisionXAthletes { get; set; }
        #endregion

        #region Construnctors
        public Sport_Division()
        {
            Sport_DivisionXSports = new HashSet<Sport_DivisionXSport>();
            Athlete_DivisionXAthletes = new HashSet<Athlete_DivisionXAthlete>();
        }
        #endregion

        /*
         * public static EN_Diviosions StrawWeight = new EN_Diviosions(10, "Paglia", 52.2);
         * public static EN_Diviosions FlyWeight = new EN_Diviosions(20, "Mosca", 56.7);
         * public static EN_Diviosions BantamWeight = new EN_Diviosions(30, "Gallo", 61.2);
         * public static EN_Diviosions FeatherWeight = new EN_Diviosions(40, "Piuma", 65.8);
         * public static EN_Diviosions LightWeight = new EN_Diviosions(50, "Leggeri", 70.3);
         * public static EN_Diviosions WelterWeight = new EN_Diviosions(60, "Welter", 77.1);
         * public static EN_Diviosions MiddleWeight = new EN_Diviosions(70, "Medi", 83.9);
         * public static EN_Diviosions LightHeavyWeight = new EN_Diviosions(80, "Massimi leggeri", 93.0);
         * public static EN_Diviosions HeavyWeight = new EN_Diviosions(90, "Massimi", 120.2);
         */
    }
}
