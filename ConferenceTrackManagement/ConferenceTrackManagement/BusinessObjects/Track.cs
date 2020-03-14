using System.Collections.Generic;

namespace ConferenceTrackManagement.BusinessObjects
{
    public class Track
    {
        private List<Slot> slots;
        public int TrackId { get; private set; }

        public Track(int trackId)
        {
            TrackId = trackId;
            slots = new List<Slot>();
        }

        public void AddSlot(Slot slot)
        {
            slots.Add(slot);
        }

        public List<Slot> GetSlots()
        {
            return slots;
        }
    }
}
