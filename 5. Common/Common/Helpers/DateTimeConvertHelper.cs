using Common.Configurations;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class DateTimeConvertHelper
    {
        public const long EpochTicks = 621355968000000000;
        public const long TicksPeriod = 10000000;
        public const long TicksPeriodMs = 10000;

        public static readonly DateTime Epoch = new DateTime(EpochTicks, DateTimeKind.Utc);

        public static DateTime LocalToUtcTime(this DateTime date)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, TimeZoneInfo.Local.Id, TimeZoneInfo.Utc.Id);
        }

        public static long ToSecondsTimestamp(this DateTime date)
        {
            if (date.Year == 9999)
            {
                return 0;
            }

            long ts = (date.Ticks - EpochTicks) / TicksPeriod;
            return ts;
        }

        public static List<DateTime> RangeTo(this DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(0, (endDate - startDate).Days + 1).Select(d => startDate.AddDays(d)).ToList();
        }    

        public static List<DateTime> GetDaysOfWeek(this DateTime date, string startWeek, string endWeek)
        {
            var dayNumbers = new List<int>
            {
                Convert.ToInt32(startWeek)
            };
            var isNotComplete = true;
            while (isNotComplete)
            {
                int value = dayNumbers.Last() + 1 == 7 ? 0 : dayNumbers.Last() + 1;
                dayNumbers.Add(value);
                if (value == Convert.ToInt32(endWeek))
                {
                    isNotComplete = false;
                }    
            }    
            var dayOfWeekIndex = dayNumbers.IndexOf(date.DayOfWeek.ToIdentityNumber());
            return date.AddDays(-dayOfWeekIndex).RangeTo(date.AddDays(6 - dayOfWeekIndex));
        }

        public static int ToIdentityNumber(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Sunday => 0,
                DayOfWeek.Monday => 1,
                DayOfWeek.Tuesday => 2,
                DayOfWeek.Wednesday => 3,
                DayOfWeek.Thursday => 4,
                DayOfWeek.Friday => 5,
                DayOfWeek.Saturday => 6,
                _ => 0,
            };
        }

        public static List<DateTime> RemoveDatesByIdentityNumbers(this List<DateTime> dates, string identityNumbers)
        {
            if (string.IsNullOrWhiteSpace(identityNumbers))
                return dates;

            var identityNumbersArr = identityNumbers.Split(",").Select(x => Convert.ToInt32(x));
            return dates.Where(x => !identityNumbersArr.Contains(x.DayOfWeek.ToIdentityNumber())).ToList();
        }

        public static async Task<List<DateTime>> RemoveHolidays(this List<DateTime> dates, GoogleDeveloperConfig googleDeveloperConfig)
        {
            var holidays = new List<DateTime>();

            // Create a service object
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = googleDeveloperConfig.ApiKey,
                ApplicationName = googleDeveloperConfig.ApplicationName,
            });

            // Create a request object
            var request = service.Events.List(googleDeveloperConfig.CalendarId);
            request.Fields = "items(start,end)";
            request.TimeMin = dates.Min();
            request.TimeMax = dates.Max();
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            try
            {
                // Execute the request and get a response object
                var response = await request.ExecuteAsync();

                // Loop through the events data
                foreach (var eventItem in response.Items)
                {
                    DateTime date = DateTime.Parse(eventItem.Start.Date);
                    holidays.Add(date);
                }
            }
            catch
            {
                throw;
            }
            return dates.Where(x => !holidays.Contains(x.Date)).ToList();
        }

        public static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }
    }
}
