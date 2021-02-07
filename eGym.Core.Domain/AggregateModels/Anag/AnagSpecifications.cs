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

        public static ASpec<Anag_Master> ByUserID(int id) => new Spec<Anag_Master>(t => t.Ang_UserID.HasValue && t.Ang_UserID.Value == id);
        public static ASpec<Anag_Master> WithCredentials(bool with) => new Spec<Anag_Master>(t => t.Ang_UserID.HasValue == with);

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
