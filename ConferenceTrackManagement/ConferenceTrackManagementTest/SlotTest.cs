using System;
using NUnit.Framework;
using ConferenceTrackManagement.BusinessObjects;
using ConferenceTrackManagement.Enums;

namespace ConferenceTrackManagementTest
{
    [TestFixture]
    public class SlotTest
    {
        private Slot _morningSlot;
        private Slot _lunchSlot;
        private Slot _eveningSlot;
        private Slot _NetworkingEvent;

        [SetUp]
        public void SetUp()
        {
            _morningSlot = new Slot( EventEnum.Morning , new SlotDetail()
                                {
                                    StartTime = new TimeSpan(9, 0, 0),
                                    MinimumDuration = 3,
                                    MaxDuration = 3
                                }, true);
            _lunchSlot = new Slot(EventEnum.Lunch, new SlotDetail()
                        {
                            StartTime = new TimeSpan(12, 0, 0),
                            MinimumDuration = 1,
                            MaxDuration = 1
                        }, false);
            _eveningSlot = new Slot(EventEnum.Evening, new SlotDetail()
                        {
                            StartTime = new TimeSpan(13, 0, 0),
                            MinimumDuration = 3,
                            MaxDuration = 4
                        }, true);
            _NetworkingEvent = new Slot(EventEnum.NetworkingEvent, new SlotDetail()
                        {
                            StartTime = new TimeSpan(17, 0, 0),
                            MinimumDuration = 1,
                            MaxDuration = 1
                        }, false);
        }

        [Test]
        public void IsSlot_IsEditable_Return_TrueforMorningandEvening_FalseforLunchandNetworking()
        {
            Assert.IsTrue(_morningSlot.Editable, "Morning Slot Is Editable");
            Assert.IsFalse(_lunchSlot.Editable, "Lunch Slot Is Editable");
            Assert.IsTrue(_eveningSlot.Editable, "Evening Slot Is Editable");
            Assert.IsFalse(_NetworkingEvent.Editable, "Networking Event Slot Is Editable");
        }

        [Test]
        public void SlotEndTime_Return_CorrectValues()
        {
            Assert.AreEqual(_morningSlot.SlotEndTime , new TimeSpan(12, 0, 0), "Morning Slot End by 12:00 pm");
            
            Assert.AreEqual(_eveningSlot.SlotEndTime, new TimeSpan(17, 0, 0), "Evening Slot End by 05:00 pm");
        }

        [Test]
        public void PreviousSession_Return_Null_ForNoSessions()
        {
            Assert.AreEqual(_morningSlot.PreviousSession, null, "Null will be returned if No Sessions in the Slot");
        }

        [Test]
        public void PreviousSession_Return_NotNull_IfSessionsNotEmpty()
        {
            var rawSession = new RawSession() { Duration = 10, Name = "Session 1" };
            _morningSlot.AddSession(rawSession);
            Assert.IsNotNull(_morningSlot.PreviousSession , "Null will be returned if No Sessions in the Slot");
        }

        [Test]
        public void PreviousSessionEndTime_Return_NotNull_IfSessionsNotEmpty()
        {
            var rawSession = new RawSession() { Duration = 10, Name = "Session 1" };
            _morningSlot.AddSession(rawSession);

            var result = _morningSlot.PreviousSessionEndTime;

            Assert.AreEqual(result, new TimeSpan(9,10,0), "Previous will be returned if No Sessions in the Slot");
        }

        [Test]
        public void PreviousSessionEndTime_Return_SlotStartTime_IfSessionsEmpty()
        {
            var result = _morningSlot.PreviousSessionEndTime;
            Assert.AreEqual(result, _morningSlot.SlotSession.StartTime, "SlotStartTime will be returned if No Sessions in the Slot");
        }

        [Test]
        public void IsSlotFull_Return_false_IfSessionsAreFUll()
        {
            Assert.IsFalse(_morningSlot.IsSlotFull, "IsSlotFull will return false when slot is empty");

            _morningSlot.AddSession(new RawSession() { Duration = 60, Name = "Session 1" });
            Assert.IsFalse(_morningSlot.IsSlotFull, "IsSlotFull will return false when slot is not full");
        }

        [Test]
        public void IsSlotFull_Return_true_IfSessionsAreFUll()
        {
            _morningSlot.AddSession(new RawSession() { Duration = 60, Name = "Session 1" });
            _morningSlot.AddSession(new RawSession() { Duration = 60, Name = "Session 2" });            
            _morningSlot.AddSession(new RawSession() { Duration = 60, Name = "Session 3" });
            Assert.IsTrue(_morningSlot.IsSlotFull, "IsSlotFull will return true when slot is completely full");
        }

        [Test]
        public void GetSessions_Return_ListOfSessions()
        {
            Assert.IsNotNull(_morningSlot.GetSessions(), "GetSessions will return empty list when no session is added to the slot");
            _morningSlot.AddSession(new RawSession() { Duration = 60, Name = "Session 1" });            
            Assert.AreEqual(_morningSlot.GetSessions().Count, 1 , "GetSessions will return  list when session is added to the slot");
        }

        [Test]
        public void IsSafe_Return_true_IfSessionsAreNotFUll()
        {
            var result = _morningSlot.IsSafe(new RawSession() { Duration = 60, Name = "Session 1" });
            Assert.IsTrue(result, "IsSafe will return true when slot is not completely full");
        }

        [Test]
        public void IsSafe_Return_False_IfSessionsAreFUll()
        {
           
            _morningSlot.AddSession(new RawSession() { Duration = 60, Name = "Session 1" });
            _morningSlot.AddSession(new RawSession() { Duration = 60, Name = "Session 1" });
            _morningSlot.AddSession(new RawSession() { Duration = 60, Name = "Session 1" });

            var result = _morningSlot.IsSafe(new RawSession() { Duration = 5, Name = "Session 1" });
            Assert.IsFalse(result, "IsSafe will return false when slot is completely full");
        }


        [Test]
        public void SlotName_Return_EnumMorningForMorningSession()
        {
            Assert.AreEqual(_morningSlot.Name, EventEnum.Morning , "Morning Slot name is same as enum morning");
        }
    }
}
