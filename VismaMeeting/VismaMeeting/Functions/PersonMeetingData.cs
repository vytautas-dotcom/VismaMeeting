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
        public void AddMeetingToPerson(Guid id, Person person)
            => person.PersonMeetings.Add(id, DateTime.Now);
        public void AddPersonToMeeting(Meeting meeting, Person person)
            => meeting.Persons.Add(person);
        public void RemoveMeetingFromPersonMeetings(Guid id, PersonList personList)
            => personList.ForEach(x => x.PersonMeetings.Remove(id));
    }
}
