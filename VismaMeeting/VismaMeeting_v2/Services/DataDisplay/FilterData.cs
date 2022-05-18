﻿using VismaMeeting_v2.Models;

namespace VismaMeeting_v2.Services.DataDisplay
{
    public class FilterData
    {
        public Dictionary<string, int> FilterParameters = new Dictionary<string, int>()
        {
            {"Description", 0},
            {"Responsible person", 1},
            {"Category", 2},
            {"Type", 3},
            {"Date", 4},
            {"Attendees number", 5}
        };
        public List<int> FilterParameterIndexes = new List<int>(new[] { 0, 1, 2, 3, 4, 5 });

        public Meetings FilterByDescription(string text, Meetings meetingList)
        {
            Meetings meetings = new Meetings();
            foreach (var meeting in meetingList)
            {
                if(meeting.Description.ToLower().Contains(text.ToLower()))
                    meetings.Add(meeting);
            }
            return meetings;
        }
        public Meetings FilterByResponsiblePerson(string text, Meetings meetingList, Persons persons)
        {
            Meetings meetings = new Meetings();
            foreach (var meeting in meetingList)
            {
                foreach (var person in persons)
                {
                    if (person.Name.ToLower().Contains(text.ToLower()) && meeting.ResponsiblePerson == person.Name)
                        meetings.Add(meeting);
                }
            }
            return meetings;
        }
        public Meetings FilterByCategory(int index, Meetings meetingList)
        {
            Meetings meetings = new Meetings();
            foreach (var meeting in meetingList)
            {
                if (meeting.Category == (MeetCategory)index)
                    meetings.Add(meeting);
            }
            return meetings;
        }
        public Meetings FilterByType(int index, Meetings meetingList)
        {
            Meetings meetings = new Meetings();
            foreach (var meeting in meetingList)
            {
                if (meeting.Type == (MeetType)index)
                    meetings.Add(meeting);
            }
            return meetings;
        }
        public Meetings FilterByDate(DateTime date, Meetings meetingList)
        {
            Meetings meetings = new Meetings();
            foreach (var meeting in meetingList)
            {
                if (meeting.StartDate <= date && date < meeting.EndDate)
                    meetings.Add(meeting);
            }
            return meetings;
        }
        public Meetings FilterByNumberOfAttendees((int, int) interval, Meetings meetingList)
        {
            Meetings meetings = new Meetings();
            if (interval.Item2 == 0)
                foreach (var meeting in meetingList)
                {
                    if (meeting.Persons.Count <= interval.Item1)
                        meetings.Add(meeting);
                }
            else if (interval.Item1 == 0)
                foreach (var meeting in meetingList)
                {
                    if (meeting.Persons.Count > interval.Item2)
                        meetings.Add(meeting);
                }
            else
                foreach (var meeting in meetingList)
                {
                    if (meeting.Persons.Count > interval.Item1 && meeting.Persons.Count <= interval.Item2)
                        meetings.Add(meeting);
                }
            return meetings;
        }
    }
}
