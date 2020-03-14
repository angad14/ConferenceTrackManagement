using ConferenceTrackManagement.Enums;
using ConferenceTrackManagement.Utilities;
using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement.BusinessObjects
{
    public class Slot
    {
        public Slot(EventEnum name, SlotDetail slotDetail, bool editable)
        {
            Name = name;
            SlotSession = slotDetail;
            _sessions = new List<Session>();
            Editable = editable;
        }

        public EventEnum Name { get; private set; }

        public SlotDetail SlotSession;

        private List<Session> _sessions;

        public bool Editable { get; private set; }

        public TimeSpan SlotEndTime { get { return SlotSession.StartTime.AddHours(SlotSession.MaxDuration); } }

        public Session PreviousSession { get { return _sessions.Count > 0 ? _sessions[_sessions.Count - 1] : null; } }

        public TimeSpan PreviousSessionEndTime { get { return PreviousSession != null ? PreviousSession.SessionEndTime : SlotSession.StartTime; } }

        public bool IsSlotFull { get { return PreviousSession != null && PreviousSessionEndTime == SlotEndTime; } }

        public bool AddSession(RawSession rawSession)
        {
            if (IsSafe(rawSession))
            {
                _sessions.Add(new Session(rawSession.Name, rawSession.Duration, PreviousSessionEndTime));
                return true;
            }
            return false;
        }

        public bool IsSafe(RawSession rawSession)
        {
            if (IsSlotFull)
                return false;

            var newTime = PreviousSessionEndTime.AddMinutes(rawSession.Duration);
            return newTime <= SlotEndTime;
        }

        public List<Session> GetSessions()
        {
            return _sessions;
        }
    }
}
