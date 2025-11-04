using System;

namespace fiexpress.Extensions
{
    public static class TimeOnlyExtensions
    {
        public static TimeSpan ToTimeSpan(this TimeOnly time) =>
            new TimeSpan(0, time.Hour, time.Minute, time.Second, time.Millisecond);
    }
}
