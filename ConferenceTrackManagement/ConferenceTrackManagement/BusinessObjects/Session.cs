using ConferenceTrackManagement.Utilities;
using System;
using ConferenceTrackManagement.Constants;

namespace ConferenceTrackManagement.BusinessObjects
{
    public class Session
    {
        public Session(string name, int duration, TimeSpan startTime)
        {
            SessionName = name;
            SessionStartTime = startTime;
            SessionDuration = duration;
        }

        public TimeSpan SessionEndTime { get { return SessionStartTime.AddMinutes(SessionDuration); } }

        public string SessionName { get; private set; }

        public int SessionDuration { get; private set; }

        public TimeSpan SessionStartTime { get; private set; }

        public string Duration { get { return SessionDuration == 5 ? TimeConstants.LIGHTINING : SessionDuration +  TimeConstants.MINUTES; } }
    }
}
