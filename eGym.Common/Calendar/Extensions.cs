using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eGym.Common.Calendar
{
    public static class Extensions
    {
        #region Privates
        private class CalendarConstants
        {
            public int? minDate { get; set; }
            public int maxDate { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }

        private static IEnumerable<CalendarConstants> Costants = new List<CalendarConstants>()
        {
            new CalendarConstants() { minDate = null , maxDate = 1582, x = 15, y= 6 },
            new CalendarConstants()  { minDate = 1583 , maxDate = 1699, x = 22, y= 2 },
            new CalendarConstants()  { minDate = 1700 , maxDate = 1799, x = 23, y= 3 },
            new CalendarConstants()  { minDate = 1800 , maxDate = 1899, x = 23, y= 4 },
            new CalendarConstants()  { minDate = 1900 , maxDate = 2099, x = 24, y= 5 },
            new CalendarConstants()  { minDate = 2100 , maxDate = 2199, x = 24, y= 6 },
            new CalendarConstants()  { minDate = 2200 , maxDate = 2299, x = 25, y= 7 },
            new CalendarConstants()  { minDate = 2300 , maxDate = 2399, x = 26, y= 1 },
            new CalendarConstants()  { minDate = 2400 , maxDate = 2499, x = 25, y= 1 }
        };
        #endregion

        public static DateTime? GetEasterDate(int year)
        {
            var constant = Costants.First(cx => (!cx.minDate.HasValue || year >= cx.minDate.Value) && (year <= cx.maxDate));
            var x = constant.x;
            var y = constant.y;
            var a = year % 19;
            var b = year % 4;
            var c = year % 7;
            var d = (19 * a + x) % 30;
            var e = (2 * b + 4 * c + 6 * d + y) % 7;
            var sum = 22 + d + e;
            if (sum <= 31)
                return new DateTime(year, 3, sum);
            else if (((sum - 31) != 26 && (sum - 31) != 25) || ((sum - 31) == 25 && (d != 28 || a <= 10)))
                return new DateTime(year, 4, sum - 31);
            else if ((sum - 31) == 25 && d == 28 && a > 10)
                return new DateTime(year, 4, 18);
            else
                return new DateTime(year, 4, 19);
        }

        public static int GetTotalDays(this MonthsEnum month, int? year = null)
        {
            var date = new DateTime((year ?? DateTime.Now.Year), (int)month, 1);
            var iterDate = new DateTime(date.Year, date.Month, 1);
            var d = 0;
            while (date.Month == iterDate.Month)
            {
                d += 1;
                iterDate = iterDate.AddDays(1);
            }
            return d;
        }

        public static IEnumerable<DateTime> GetFestivityInYear(this DateTime date) => date.Year.GetFestivityInYear();

        public static IEnumerable<DateTime> GetFestivityInYear(this int year)
        {
            return new List<DateTime>
            {
                new DateTime(year, 1, 1).Date,
                new DateTime(year, 1, 6).Date,
                new DateTime(year, 4, 25).Date,
                new DateTime(year, 5, 1).Date,
                new DateTime(year, 6, 2).Date,
                new DateTime(year, 8, 15).Date,
                new DateTime(year, 11, 1).Date,
                new DateTime(year, 12, 8).Date,
                new DateTime(year, 12, 25).Date,
                new DateTime(year, 12, 26).Date,
                ((DateTime)GetEasterDate(year)).Date,
                ((DateTime)GetEasterDate(year)).AddDays(1).Date
            }
            .OrderBy(d => d)
            .ToList();
        }

        public static int GetMonthFestivity(this MonthsEnum month, int? year = null)
        {
            return (new DateTime((year ?? DateTime.Now.Year), (int)month, 1)).Year.GetFestivityInYear()
                .Where(m => m.Month == (int)month)
                .Count();
        }

        private static int GetDayWeekNumber(DateTime date)
        {
            // la sett. uno è quella con il primo giovedi dell'anno
            int actDoY = date.DayOfYear;
            int actDoW = (int)date.DayOfWeek;
            actDoW = actDoW == 0 ? 7 : actDoW;
            DateTime firstOfyear = new DateTime(date.Year, 1, 1);
            int frst = (int)firstOfyear.DayOfWeek;
            int d = frst > 0 && frst <= 4 ? frst++ : (9 - frst);
            int frstDoW = d == 9 ? 2 : d;
            return (((actDoY - (actDoW - 1)) - frstDoW) / 7);
        }

        public static IEnumerable<int> GetMonthWeekNumber(this MonthsEnum month, int? year = null)
        {
            var date = new DateTime((year ?? DateTime.Now.Year), (int)month, 1);
            var iterDate = new DateTime(date.Year, date.Month, 1);
            var rtn = new List<int>();
            while (date.Month == iterDate.Month)
            {
                rtn.Add(GetDayWeekNumber(iterDate));
                iterDate = iterDate.AddDays(1);
            }
            return rtn.Distinct().ToList();
        }
    }
}
