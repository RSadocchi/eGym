using eGym.Core.SeedWork;

namespace eGym.Core.Localization
{
    public interface ICMSRepository : IRepository<CMS_Master, int>
    {
        string this[(string groupKey, string contextualKey) key] { get; }
        string this[(string groupKey, string contextualKey) key, params object[] arguments] { get; }
        string this[(string groupKey, string contextualKey, string culture) key] { get; }
        string this[(string groupKey, string contextualKey, string culture) key, params object[] arguments] { get; }
    }
}
