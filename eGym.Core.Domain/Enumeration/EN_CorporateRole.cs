using eGym.Core.SeedWork;
using System.Collections.Generic;

namespace eGym.Core.Domain
{
    public class EN_CorporateRole : Enumeration<short>
    {
        public List<EN_AnagMasterRole> AllowedInRoles { get; set; } = null;

        public EN_CorporateRole() : base() { }
        public EN_CorporateRole(short id, string code) : base(id, code) { }

        public static IEnumerable<EN_CorporateRole> GetAll() => GetAll<EN_CorporateRole>();
        public static EN_CorporateRole FromID(short id) => FromID<EN_CorporateRole>(id);

        public static EN_CorporateRole President = new EN_CorporateRole(1, nameof(President))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee }
        };
        
        public static EN_CorporateRole VicePresident = new EN_CorporateRole(2, nameof(VicePresident))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee }
        };
        
        public static EN_CorporateRole Treasurer = new EN_CorporateRole(3, nameof(Treasurer))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee }
        };
        
        public static EN_CorporateRole Secretary = new EN_CorporateRole(4, nameof(Secretary))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee }
        };
        
        public static EN_CorporateRole Adviser = new EN_CorporateRole(5, nameof(Adviser))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee }
        };
        
        public static EN_CorporateRole AthleteAdviser = new EN_CorporateRole(6, nameof(AthleteAdviser))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee }
        };
        
        public static EN_CorporateRole Coach = new EN_CorporateRole(7, nameof(Coach))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee, EN_AnagMasterRole.Supplier }
        };
        
        public static EN_CorporateRole CoachAssistant = new EN_CorporateRole(8, nameof(CoachAssistant))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee, EN_AnagMasterRole.Supplier }
        };
        
        public static EN_CorporateRole Trainer = new EN_CorporateRole(9, nameof(Trainer))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee, EN_AnagMasterRole.Supplier }
        };
        
        public static EN_CorporateRole TrainerAssistant = new EN_CorporateRole(10, nameof(TrainerAssistant))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee, EN_AnagMasterRole.Supplier }
        };
        
        public static EN_CorporateRole Athlete = new EN_CorporateRole(11, nameof(Athlete))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee }
        };
        
        public static EN_CorporateRole CompetitiveAthlete = new EN_CorporateRole(12, nameof(CompetitiveAthlete))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee }
        };
        
        public static EN_CorporateRole Doctor = new EN_CorporateRole(13, nameof(Doctor))
        {
            AllowedInRoles = new List<EN_AnagMasterRole>() { EN_AnagMasterRole.Affiliate, EN_AnagMasterRole.Employee, EN_AnagMasterRole.Supplier }
        };
    }
}
