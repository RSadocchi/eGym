using eGym.Common.Attributes;

namespace eGym.Common.Calendar
{
    public enum DaysOfWeekEnum
    {
        [Localizer("Sunday", "en", 0), Localizer("Domenica", "it", 7)] Sunday = 0,
        [Localizer("Monday", "en", 1), Localizer("Lunedi", "it", 1)] Monday,
        [Localizer("Tuesday", "en", 2), Localizer("Martedi", "it", 2)] Tuesday,
        [Localizer("Wednesday", "en", 3), Localizer("Mercoledi", "it", 3)] Wednesday,
        [Localizer("Thursday", "en", 4), Localizer("Giovedi", "it", 4)] Thursday,
        [Localizer("Friday", "en", 5), Localizer("Venerdi", "it", 5)] Friday,
        [Localizer("Saturday", "en", 6), Localizer("Sabato", "it", 6)] Saturday
    }
}
