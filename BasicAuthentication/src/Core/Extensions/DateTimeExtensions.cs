using System;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUTCToLocal(this DateTime utcTime, string DisplayTimeZone = "India Standard Time")
        {
            TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById(DisplayTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, info);
        }
    }
}
