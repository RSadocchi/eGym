using eGym.Core.SeedWork.NSpecifications;
using System;
using System.Linq;

namespace eGym.Core.Domain
{
    public static class AnagSpecifications
    {
        public static ASpec<Anag_Master> ByID(int id) => new Spec<Anag_Master>(t => t.Ang_ID == id);
        public static ASpec<Anag_Master> ByIDs(params int[] ids) => new Spec<Anag_Master>(t => ids == null || ids.Length <= 0 || ids.Contains(t.Ang_ID));
        
        public static ASpec<Anag_Master> ByGenderID(short id) => new Spec<Anag_Master>(t => t.Ang_GenderID == id);

        public static ASpec<Anag_Master> ByRoleID(short id) => new Spec<Anag_Master>(t => t.Anag_MasterRoles.Where(a => a.AngR_RoleID == id).Any());
        public static ASpec<Anag_Master> ByRoleIDs(params short[] ids) => new Spec<Anag_Master>(t => t.Anag_MasterRoles.Where(a => ids.Contains(a.AngR_RoleID)).Any());

        public static ASpec<Anag_Master> ByCorporateRoleID(short id) => new Spec<Anag_Master>(t => t.Anag_CorporateRoles.Where(a => a.CR_RoleID == id).Any());
        public static ASpec<Anag_Master> ByCorporateRoleIDs(params short[] ids) => new Spec<Anag_Master>(t => t.Anag_CorporateRoles.Where(a => ids.Contains(a.CR_RoleID)).Any());

        public static ASpec<Anag_Master> ByUserID(int id) => new Spec<Anag_Master>(t => t.Ang_UserID.HasValue && t.Ang_UserID.Value == id);
        public static ASpec<Anag_Master> WithCredentials(bool hasCredentials) => new Spec<Anag_Master>(t => t.Ang_UserID.HasValue == hasCredentials);

        public static ASpec<Anag_Master> ByAthleteID(int id) => new Spec<Anag_Master>(t => t.Athlete_Master != null && t.Athlete_Master.Ath_ID == id);
        public static ASpec<Anag_Master> ByAthleteIDs(params int[] ids) => new Spec<Anag_Master>(t => ids == null || ids.Length <= 0 || (t.Athlete_Master != null && ids.Contains(t.Athlete_Master.Ath_ID)));

        public static ASpec<Anag_Master> ByTaxCode(string search = null, SearchStringKindEnum stringKind = SearchStringKindEnum.Contains)
            => stringKind switch
            {
                SearchStringKindEnum.StartsWith =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.Ang_TaxCode) && t.Ang_TaxCode.Trim().StartsWith(search.Trim(), StringComparison.InvariantCultureIgnoreCase)),
                SearchStringKindEnum.EndsWith =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.Ang_TaxCode) && t.Ang_TaxCode.Trim().EndsWith(search.Trim(), StringComparison.InvariantCultureIgnoreCase)),
                SearchStringKindEnum.Match =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.Ang_TaxCode) && t.Ang_TaxCode.Trim().Equals(search.Trim(), StringComparison.InvariantCultureIgnoreCase)),
                _ => 
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.Ang_TaxCode) && t.Ang_TaxCode.Trim().Contains(search.Trim(), StringComparison.InvariantCultureIgnoreCase))
            };

        public static ASpec<Anag_Master> ByVATNo(string search = null, SearchStringKindEnum stringKind = SearchStringKindEnum.Contains)
            => stringKind switch
            {
                SearchStringKindEnum.StartsWith =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.Ang_VATNo) && t.Ang_VATNo.Trim().StartsWith(search.Trim(), StringComparison.InvariantCultureIgnoreCase)),
                SearchStringKindEnum.EndsWith =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.Ang_VATNo) && t.Ang_VATNo.Trim().EndsWith(search.Trim(), StringComparison.InvariantCultureIgnoreCase)),
                SearchStringKindEnum.Match =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.Ang_VATNo) && t.Ang_VATNo.Trim().Equals(search.Trim(), StringComparison.InvariantCultureIgnoreCase)),
                _ =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.Ang_VATNo) && t.Ang_VATNo.Trim().Contains(search.Trim(), StringComparison.InvariantCultureIgnoreCase))
            };

        public static ASpec<Anag_Master> ByCompleteName(string search = null, SearchStringKindEnum stringKind = SearchStringKindEnum.Contains)
            => stringKind switch
            {
                SearchStringKindEnum.StartsWith =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.CompleteName) && t.CompleteName.Trim().StartsWith(search.Trim(), StringComparison.InvariantCultureIgnoreCase)),
                SearchStringKindEnum.EndsWith =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.CompleteName) && t.CompleteName.Trim().EndsWith(search.Trim(), StringComparison.InvariantCultureIgnoreCase)),
                SearchStringKindEnum.Match =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.CompleteName) && t.CompleteName.Trim().Equals(search.Trim(), StringComparison.InvariantCultureIgnoreCase)),
                _ =>
                    new Spec<Anag_Master>(t => !string.IsNullOrWhiteSpace(search) && !string.IsNullOrWhiteSpace(t.CompleteName) && t.CompleteName.Trim().Contains(search.Trim(), StringComparison.InvariantCultureIgnoreCase))
            };
    }
}
