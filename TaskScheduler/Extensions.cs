namespace TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Extensions
    {
        public static DateTime Max(this DateTime minStartDate, List<AbstractTask> list)
        {
            var endDateList = new List<DateTime>();
            for (int i = 0; i < list.Count; i++)
            {
                endDateList.Add(list[i].EndDate);
            }

            DateTime max = minStartDate;
            foreach (var date in endDateList)
            {
                if (DateTime.Compare(date.AddDays(1), max) == 1)
                {
                    max = date.AddDays(1);
                }
            }

            return max;
        }

        public static DateTime AddWorkDays(this DateTime date, int workingDays)
        {
            int direction = workingDays < 0 ? -1 : 1;
            DateTime newDate = date;
            while (workingDays != 0)
            {
                newDate = newDate.AddDays(direction);
                if (newDate.DayOfWeek != DayOfWeek.Saturday &&
                    newDate.DayOfWeek != DayOfWeek.Sunday &&
                    !newDate.IsHoliday())
                {
                    workingDays -= direction;
                }
            }

            return newDate;
        }

        public static bool IsHoliday(this DateTime date)
        {
            // You'd load/cache from a DB or file somewhere rather than hardcode
            DateTime[] holidays =
            new DateTime[]
            {
            new DateTime(2010, 12, 27),
            new DateTime(2010, 12, 28),
            new DateTime(2011, 01, 03),
            new DateTime(2011, 01, 12),
            new DateTime(2011, 01, 13),
            };

            return holidays.Contains(date.Date);
        }

        public static DateTime Min(this IEnumerable<DateTime> dates)
        {
            DateTime min = dates.First();
            foreach (var date in dates)
            {
                if (date < min)
                {
                    min = date;
                }
            }

            return min;
        }

        public static DateTime MaxEndDate(this IEnumerable<DateTime> dates)
        {
            DateTime max = dates.First();
            foreach (var date in dates)
            {
                if (date > max)
                {
                    max = date;
                }
            }

            return max;
        }

        public static List<T> Join<T>(this List<T> first, List<T> second)
        {
            if (first == null)
            {
                return second;
            }

            if (second == null)
            {
                return first;
            }

            return first.Concat(second).ToList();
        }
    }
}