using VismaMeeting.Employees;
using VismaMeeting.MeetingData;

namespace VismaMeeting.Functions
{
    internal class PersonMeetingData
    {
        public void AddResponsiblePersonToMeeting(Meeting meeting, Person person)
        {
            if(person.PersonMeetings == null)
            {
                person.PersonMeetings = new Dictionary<Guid, DateTime>();
                person.PersonMeetings.Add(meeting.Id, DateTime.Now);
            }
            else
            person.PersonMeetings.Add(meeting.Id, DateTime.Now);
        }
    }
}
