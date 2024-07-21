namespace BS.Web.Services.Utlity
{
    public class DateTimeHelper
    {
        public static DateTime FindMaxDate(List<DateTime> dates)
        {
            if (dates == null || !dates.Any())
            {
                throw new ArgumentException("The list of dates cannot be null or empty.");
            }

            DateTime maxDate = dates[0];
            foreach (DateTime date in dates)
            {
                if (date > maxDate)
                {
                    maxDate = date;
                }
            }
            return maxDate;
        }
        public static DateTime FindMinDate(List<DateTime> dates)
        {
            if (dates == null || !dates.Any())
            {
                throw new ArgumentException("The list of dates cannot be null or empty.");
            }

            DateTime minDate = dates[0];
            foreach (DateTime date in dates)
            {
                if (date < minDate)
                {
                    minDate = date;
                }
            }
            return minDate;
        }
    }
}
