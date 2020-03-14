using System;

namespace ConferenceTrackManagement.BusinessObjects
{
    public class SlotDetail
    {
        public TimeSpan StartTime { get; set; }
        public int MinimumDuration { get; set; }
        public int MaxDuration { get; set; }
    }
}
