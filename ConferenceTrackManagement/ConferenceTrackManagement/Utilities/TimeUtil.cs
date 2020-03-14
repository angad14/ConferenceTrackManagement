using System;

namespace ConferenceTrackManagement.Utilities
{
    public static class TimeUtil
    {
        public static TimeSpan AddMinutes(this TimeSpan timeObject, int minutes)
        {
            return timeObject.Add(new TimeSpan(0, minutes, 0));
        }

        public static TimeSpan AddHours(this TimeSpan timeObject, int hours)
        {
            return timeObject.Add(new TimeSpan(hours, 0, 0));
        }

        public static string Format(this TimeSpan timeObject, string format)
        {
            DateTime time = DateTime.Today + timeObject;
            return time.ToString("hh:mm tt");
        }
    }
}
