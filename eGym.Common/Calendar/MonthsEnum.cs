using eGym.Common.Attributes;

namespace eGym.Common.Calendar
{
    public enum MonthsEnum
    {
        [Localizer("Janary", "en"), Localizer("Gennaio", "it")] January = 1,
        [Localizer("February", "en"), Localizer("Febbraio", "it")] February,
        [Localizer("March", "en"), Localizer("Marzo", "it")] March,
        [Localizer("April", "en"), Localizer("Aprile", "it")] April,
        [Localizer("May", "en"), Localizer("Maggio", "it")] May,
        [Localizer("June", "en"), Localizer("Giugno", "it")] June,
        [Localizer("July", "en"), Localizer("Luglio", "it")] July,
        [Localizer("August", "en"), Localizer("Agosto", "it")] August,
        [Localizer("September", "en"), Localizer("Settembre", "it")] September,
        [Localizer("October", "en"), Localizer("Ottobre", "it")] October,
        [Localizer("November", "en"), Localizer("Novembre", "it")] November,
        [Localizer("December", "en"), Localizer("Dicembre", "it")] December
    }
}
