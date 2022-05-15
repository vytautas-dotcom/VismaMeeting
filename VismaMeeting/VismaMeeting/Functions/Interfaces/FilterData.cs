using VismaMeeting.Employees;
using VismaMeeting.MeetingData;

namespace VismaMeeting.Functions.Interfaces
{
    internal class FilterData
    {
        private readonly PersonList _personList;
        public FilterData()
        {

        }
        public FilterData(PersonList personList)
        {
            _personList = personList;
        }

        public Dictionary<string, int> FilterParameters = new Dictionary<string, int>()
        {
            {"Description", 0},
            {"Responsible person", 1},
            {"Category", 2},
            {"Type", 3},
            {"Date", 4},
            {"Attendees", 5}
        };
        public List<int> FilterParameterIndexes = new List<int>(new[] { 0, 1, 2, 3, 4, 5 });

        public MeetingList FilterByDescription(string text, MeetingList meetingList)
        {
            MeetingList meetings = new MeetingList();
            foreach (var meeting in meetingList)
            {
                if(meeting.Description.ToLower().Contains(text.ToLower()))
                    meetings.Add(meeting);
            }
            return meetings;
        }
        public MeetingList FilterByResponsiblePerson(string text, MeetingList meetingList)
        {
            MeetingList meetings = new MeetingList();
            foreach (var meeting in meetingList)
            {
                foreach (var person in _personList)
                {
                    if (person.Name.ToLower().Contains(text.ToLower()) && meeting.ResponsiblePersonId == person.Id)
                        meetings.Add(meeting);
                }
            }
            return meetings;
        }
        public MeetingList FilterByCategory(string number, MeetingList meetingList)
        {
            MeetingList meetings = new MeetingList();

            foreach (var meeting in meetingList)
            {
                foreach (var person in _personList)
                {
                    if (person.Name.ToLower().Contains(text.ToLower()) && meeting.ResponsiblePersonId == person.Id)
                        meetings.Add(meeting);
                }
            }
            return meetings;
        }
    }
}
