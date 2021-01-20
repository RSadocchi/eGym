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

        #endregion

        #region Virtuals
        public virtual ICollection<Sport_Schedule> Sport_Schedules { get; set; }
        public virtual ICollection<Athlete_Master> Athlete_Masters { get; set; }
        #endregion

        #region Construnctors
        public Sport_Master()
        {
            Sport_Schedules = new HashSet<Sport_Schedule>();
            Athlete_Masters = new HashSet<Athlete_Master>();
        }
        #endregion
    }

    [Table(nameof(Sport_Schedule), Schema = "conf")]
    public class Sport_Schedule : Entity
    {
        #region Db Columns

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

    [Table(nameof(Sport_Division), Schema = "conf")]
    public class Sport_Division : Entity
    {
        #region Db Columns

        #endregion

        #region Virtuals
        public virtual Sport_Master Sport_Master { get; set; }
        public virtual ICollection<Athlete_DivisionXAthlete> Athlete_DivisionXAthletes { get; set; }
        #endregion

        #region Construnctors
        public Sport_Division()
        {
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

    [Table(nameof(Sport_Level), Schema = "conf")]
    public class Sport_Level : Entity
    {
        #region Db Columns

        #endregion

        #region Virtuals

        #endregion

        #region Construnctors

        #endregion
    }

    [Table(nameof(Sport_EventResult), Schema = "conf")]
    public class Sport_EventResult : Entity
    {
        #region Db Columns

        #endregion

        #region Virtuals

        #endregion

        #region Construnctors

        #endregion

        /*
         * public static EN_ResultTypes Lose = new EN_ResultTypes(10, "Sconfitta");
         * public static EN_ResultTypes Pair = new EN_ResultTypes(20, "Pareggio");
         * public static EN_ResultTypes Win = new EN_ResultTypes(30, "Vittoria");
         */
    }

    [Table(nameof(Sport_EventResultType), Schema = "conf")]
    public class Sport_EventResultType : Entity
    {
        #region Db Columns

        #endregion

        #region Virtuals

        #endregion

        #region Construnctors

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
