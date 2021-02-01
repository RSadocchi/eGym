using eGym.Core.SeedWork;
using eGym.Core.SeedWork.NSpecifications;
using System;
using System.Globalization;
using System.Linq;

namespace eGym.Core.Localization
{
    public class CMSRepository : BaseRepository<LocalizationDbContext, CMS_Master, int>, ICMSRepository
    {
        public CMSRepository(LocalizationDbContext context) : base(context) { }

        public string this[(string groupKey, string contextualKey) key] 
            => FindBy(groupKey: key.groupKey, contextualKey: key.contextualKey)?.CMS_Value ?? $"{key.groupKey}.{key.contextualKey}";

        public string this[(string groupKey, string contextualKey, string culture) key] 
            => FindBy(groupKey: key.groupKey, contextualKey: key.contextualKey, culture: key.culture)?.CMS_Value ?? $"{key.groupKey}.{key.contextualKey}";

        public string this[(string groupKey, string contextualKey) key, params object[] arguments] 
            => string.Format((FindBy(groupKey: key.groupKey, contextualKey: key.contextualKey)?.CMS_Value ?? $"{key.groupKey}.{key.contextualKey}"), arguments);

        public string this[(string groupKey, string contextualKey, string culture) key, params object[] arguments]
            => string.Format((FindBy(groupKey: key.groupKey, contextualKey: key.contextualKey, culture: key.culture)?.CMS_Value ?? $"{key.groupKey}.{key.contextualKey}"), arguments);

        private bool MatchCulture(string entityCulture, string culture = null)
        {
            if (string.IsNullOrWhiteSpace(culture)) culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            if (entityCulture.Equals(culture, StringComparison.InvariantCultureIgnoreCase)) return true;
            if (entityCulture.StartsWith(culture, StringComparison.InvariantCultureIgnoreCase)) return true;
            if (culture.StartsWith(entityCulture, StringComparison.InvariantCultureIgnoreCase)) return true;
            return false;
        }

        private CMS_Master FindBy(string groupKey, string contextualKey, string culture = null)
        {
            ASpec<CMS_Master> spec =
                new Spec<CMS_Master>(t => t.CMS_GroupKey.Equals(groupKey, StringComparison.InvariantCultureIgnoreCase)) &
                new Spec<CMS_Master>(t => t.CMS_ContextKey.Equals(contextualKey, StringComparison.InvariantCultureIgnoreCase)) &
                new Spec<CMS_Master>(t => MatchCulture(t.CMS_Culture, culture));

            var query = GetBySpecification(spec);
            return query?.FirstOrDefault();
        }
    }
}
