using System.Reflection;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.UI;
using System.Linq;

namespace VismaMeeting_v2.Services.DataOperations
{
    public class PersonMeetingData
    {
        private readonly MessagesData _messagesData;
        private readonly DataInput _dataInput;
        private readonly UIMessages _uIMessages;
        private readonly DataChecking _dataChecking;
        private readonly ControlPanel _controlPanel;
        private readonly MeetingShowData _meetingShowData;
        private readonly PersonShowData _personShowData;
        public PersonMeetingData(DataInput dataInput, DataChecking dataChecking, MeetingShowData meetingShowData, PersonShowData personShowData)
        {
            _messagesData = new MessagesData();
            _dataInput = dataInput;
            _uIMessages = new UIMessages();
            _dataChecking = dataChecking;
            _controlPanel = new ControlPanel();
            _meetingShowData = meetingShowData;
            _personShowData = personShowData;
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
        public void DeleteMeeting(Meetings meetings, Persons persons, Person person)
        {
            bool isToDelete = false;
            if (meetings.Count > 0)
            {
                _meetingShowData.ShowNamesIndexes(meetings);
                int index;
                _dataInput.InputNumber("Number", _messagesData.WarningMessages["InputWarning"], out index);
                isToDelete = _dataChecking.IsMeetigToDeleteForPerson(meetings, person, index);

                if (!isToDelete)
                    DeleteMeeting(meetings, persons, person);
                else
                {
                    person.PersonMeetings.Remove(meetings[index].Id);
                    persons.ForEach(person => person.PersonMeetings.Remove(meetings[index].Id));
                    meetings.ForEach(x => x.Persons.ForEach(x => x.PersonMeetings.Remove(meetings[index].Id)));
                    meetings.RemoveAt(meetings.FindIndex(x => x.Id == meetings[index].Id));
                }
            }
            else
            {
                Console.Clear();
                _uIMessages.WarningMessage(_messagesData.WarningMessages["NoMeetingsToDelete"]);
                _controlPanel.Run();
            }
        }
        public void AddPerson(Meetings meetings, Persons persons)
        {
            bool isToAdd = false;
            if (meetings.Count > 0)
            {
                _meetingShowData.ShowNamesIndexes(meetings);
                int meetingIndex;
                _dataInput.InputNumber("Number", _messagesData.WarningMessages["InputWarning"], out meetingIndex);

                isToAdd = !_dataChecking.IsMeetingPersonsListFull(persons, meetings[meetingIndex]) && 
                    _dataChecking.IsSelectedIndexNotOutTheRange(meetingIndex, meetings);

                if (!isToAdd)
                    AddPerson(meetings, persons);
                else
                {
                    int personIndex;
                    List<Person> personsNotAddedYet = SelectNotAddedPersons(meetings[meetingIndex].Id, persons);
                    _meetingShowData.ShowOneItem(meetings[meetingIndex]);
                    _personShowData.ShowNamesIndexesNotAddedYet(persons, personsNotAddedYet);

                    _dataInput.InputNumber("Number", _messagesData.WarningMessages["InputWarning"], out personIndex);

                    isToAdd = _dataChecking.IsSelectedIndexNotOutTheRange(personIndex, persons);

                    if (!isToAdd)
                        AddPerson(meetings, persons);
                    else
                    {
                        persons[personIndex].PersonMeetings.Add(meetings[meetingIndex].Id, DateTime.Now);
                        AddMeetingToPersonForechMeeting(meetings, meetings[meetingIndex].Id, persons[personIndex].Id);
                        meetings[meetingIndex].Persons.Add(persons[personIndex]);
                    }
                }
            }
            else
            {
                Console.Clear();
                _uIMessages.WarningMessage(_messagesData.WarningMessages["NoMeetingsToDelete"]);
                _controlPanel.Run();
            }
        }
        static List<Person> SelectNotAddedPersons(Guid id, Persons persons)
        {
            List<Person> personsList = new List<Person>();
            foreach (var item in persons)
                if (item.PersonMeetings.ContainsKey(id))
                    personsList.Add(item);
            return personsList;
        }
        static void AddMeetingToPersonForechMeeting(Meetings meetings, Guid meetingId, Guid personId)
        {
            foreach (var meeting in meetings)
                foreach (var person in meeting.Persons)
                    if (person.Id == personId)
                        person.PersonMeetings.Add(meetingId, DateTime.Now);
        }
    }
}
