using ConferenceTrackManagement.BusinessObjects;
using ConferenceTrackManagement.Enums;
using ConferenceTrackManagement.Parser;
using ConferenceTrackManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public class TrackManagement
    {
        List<Track> _tracks;
        public string Name { get; private set; }
        private int numberOfTrack = 0;
        List<RawSession> _rawSessions;
        BaseParser basicParser;

        public TrackManagement(string trackManagementName, BaseParser parser)
        {
            Name = trackManagementName;
            _tracks = new List<Track>();
            basicParser = parser;
        }

        public List<Track> GetTracks()
        {
            return _tracks;
        }

        public Track CreateNewTrack()
        {
            numberOfTrack += 1;

            var track = new Track(numberOfTrack);

            foreach (var item in CreateSlots())
            {
                track.AddSlot(item);
            }

            _tracks.Add(track);

            return track;
        }

        public void Schedule()
        {
            _rawSessions = basicParser.GetInputSet();

            while (_rawSessions.Count > 0)
            {
                foreach (var track in GetTracks())
                {
                    AddSessionToTrack(track);
                }

                if (_rawSessions.Count > 0)
                {
                    CreateNewTrack();
                }
            }
        }

        public void AddSessionToTrack(Track track)
        {
            foreach (var rawSession in _rawSessions.ToList())
            {
                var slots = track.GetSlots();

                foreach (var slot in slots)
                {
                    if (slot.Editable && AddSessionToSlot(slot, rawSession))
                    {
                        _rawSessions.Remove(rawSession);
                        break;
                    }
                }
            }
        }

        public bool AddSessionToSlot(Slot slot, RawSession rawSession)
        {
            return slot.AddSession(rawSession);
        }

        private List<Slot> CreateSlots()
        {
            var slots = new List<Slot>();
            //Morning Slot
            slots.Add(new Slot(EventEnum.Morning, new SlotDetail()
            {
                StartTime = new TimeSpan(9, 0, 0),
                MinimumDuration = 3,
                MaxDuration = 3
            }, true));

            //Lunch Slot
            slots.Add(new Slot(EventEnum.Lunch, new SlotDetail()
            {
                StartTime = new TimeSpan(12, 0, 0),
                MinimumDuration = 1,
                MaxDuration = 1
            }, false));

            //Evening Slot
            slots.Add(new Slot(EventEnum.Evening, new SlotDetail()
            {
                StartTime = new TimeSpan(13, 0, 0),
                MinimumDuration = 3,
                MaxDuration = 4
            }, true));

            //NetworkingEvent Slot
            slots.Add(new Slot(EventEnum.NetworkingEvent, new SlotDetail()
            {
                StartTime = new TimeSpan(17, 0, 0),
                MinimumDuration = 1,
                MaxDuration = 1
            }, false));

            return slots;
        }

        public void PrintSchedule()
        {
            foreach (var item in _tracks)
            {
                Console.WriteLine($"TRACK {item.TrackId}");
                foreach (var slot in item.GetSlots())
                {
                    if (!slot.Editable)
                    {
                        Console.WriteLine($"{slot.PreviousSessionEndTime.Format("hh:mm tt")} {slot.Name}");
                    }
                    foreach (var session in slot.GetSessions())
                    {
                        Console.WriteLine($"{session.SessionStartTime.Format("hh:mm tt")} {session.SessionName} {session.Duration}");
                    }
                }
            }
        }

    }
}
