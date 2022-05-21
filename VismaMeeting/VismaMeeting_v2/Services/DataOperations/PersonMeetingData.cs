using System.Reflection;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Services.Messages;

namespace VismaMeeting_v2.Services.DataOperations
{
    public class PersonMeetingData
    {
        private readonly MessagesData _messagesData;
        private readonly DataInput _dataInput;
        private readonly UIMessages _uIMessages;
        public PersonMeetingData(DataInput dataInput)
        {
            _messagesData = new MessagesData();
            _dataInput = dataInput;
            _uIMessages = new UIMessages();
        }
        public Meeting CreateMeeting(Person person)
        {
            Meeting meeting = new Meeting();
            meeting.Id = Guid.NewGuid();
            meeting.ResponsiblePerson = person.Name;
            person.PersonMeetings.Add(meeting.Id, DateTime.Now);
            meeting.Persons = new List<Person>();

            foreach (PropertyInfo prop in meeting.GetType().GetProperties())
            {
                if (_messagesData.MeetingCreateMessages.ContainsKey(prop.Name) && prop.PropertyType.Name == "String")
                    prop.SetValue(meeting, _dataInput.InputString(prop.Name, _messagesData.WarningMessages["InputWarning"]));
                if (_messagesData.MeetingCreateMessages.ContainsKey(prop.Name) && prop.PropertyType.Name == "MeetCategory")
                {
                    int index;
                    _dataInput.EnumInput<MeetCategory>(prop.Name, _messagesData.WarningMessages["InputWarning"], out index);
                    prop.SetValue(meeting, (MeetCategory)index);
                }
                if (_messagesData.MeetingCreateMessages.ContainsKey(prop.Name) && prop.PropertyType.Name == "MeetType")
                {
                    int index;
                    _dataInput.EnumInput<MeetType>(prop.Name, _messagesData.WarningMessages["InputWarning"], out index);
                    prop.SetValue(meeting, (MeetType)index);
                }
                if (_messagesData.MeetingCreateMessages.ContainsKey(prop.Name) && prop.PropertyType.Name == "DateTime")
                {
                    DateTime output;
                    _dataInput.InputDate(prop.Name, _messagesData.WarningMessages["InputWarning"], out output);
                    prop.SetValue(meeting, output);
                }
                if (meeting.EndDate < meeting.StartDate)
                {
                    while (meeting.EndDate < meeting.StartDate && meeting.EndDate != default)
                    {
                        DateTime output;
                        _uIMessages.WarningMessage(_messagesData.WarningMessages["DateWarning"]);
                        _dataInput.InputDate(prop.Name, _messagesData.WarningMessages["DateWarning"], out output);
                        meeting.GetType().GetRuntimeProperty("EndDate").SetValue(meeting, output);
                    }
                }
            }
            return meeting;
        }
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
        public void RemoveMeetingFromPersonMeetings(Guid id, Persons personList)
            => personList.ForEach(x => x.PersonMeetings.Remove(id));
        public void RemoveMeetingFromPersonMeetings(Guid id, Person person)
            => person.PersonMeetings.Remove(id);
        public void RemovePersonFromMeeting(Guid id, Meeting meeting)
        {
            var meetingToRemove = meeting.Persons.Find(x => x.Id == id);
            meeting.Persons.Remove(meetingToRemove);
        }

    }
}
