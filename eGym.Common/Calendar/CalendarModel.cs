using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Common.Calendar
{
    public class CalendarElementModel
    {
        private DateTime _firstDay { get; set; }

        public MonthsEnum Month { get; private set; }
        public int NumberOfDays { get { return Month.GetTotalDays(_firstDay.Year); } }
        public int NumberOfFestivities { get { return Month.GetMonthFestivity(_firstDay.Year); } }
        public int WorkingDays { get { return NumberOfDays - NumberOfFestivities; } }
        public IEnumerable<int> Weeknumbers { get { return Month.GetMonthWeekNumber(_firstDay.Year); } }

        public CalendarElementModel(int year, MonthsEnum month)
        {
            Month = month;
            _firstDay = new DateTime(year, (int)month, 1, 0, 0, 0);
        }

    }

    public class CalendarModel
    {
        private DateTime _firstDay { get; set; }

        public IList<CalendarElementModel> Months { get; set; }
        public bool IsLeap { get { return DateTime.IsLeapYear(_firstDay.Year); } }
        public IEnumerable<DateTime> Festivities { get { return _firstDay.Year.GetFestivityInYear(); } }

        public CalendarModel(int year)
        {
            _firstDay = new DateTime(year, 1, 1);
            Months = new List<CalendarElementModel>();
            foreach (var m in Enum.GetValues(typeof(MonthsEnum)))
                Months.Add(new CalendarElementModel(year: year, month: (MonthsEnum)m));
        }
    }
}
