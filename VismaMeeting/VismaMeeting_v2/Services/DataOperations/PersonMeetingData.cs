using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.UI;
using System.Reflection;
using System.Linq;

namespace VismaMeeting_v2.Services.DataOperations
{
    public class PersonMeetingData
    {

        public delegate void PersonMeetingDataHandler(string message);
        public event PersonMeetingDataHandler Notify;
        private readonly MeetingShowData _meetingShowData;
        private readonly PersonShowData _personShowData;
        private readonly MessagesData _messagesData;
        private readonly DataChecking _dataChecking;
        private readonly ControlPanel _controlPanel;
        private readonly UIMessages _uIMessages;
        private readonly DataInput _dataInput;
        public PersonMeetingData(DataInput dataInput, DataChecking dataChecking, MeetingShowData meetingShowData, PersonShowData personShowData)
        {
            _dataInput = dataInput;
            _dataChecking = dataChecking;
            _meetingShowData = meetingShowData;
            _personShowData = personShowData;
            _uIMessages = new UIMessages();
            _controlPanel = new ControlPanel();
            _messagesData = new MessagesData();
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
        public void SaveMeetingPerson(Meetings _meetings, Meeting meeting, Persons _persons, Person person)
        {
            //add meeting to person
            int personIndex = _persons.FindIndex(x => x.Id == person.Id);
            _persons.RemoveAt(personIndex);
            _persons.Add(person);
            //_dbServiceP.Save(_persons);

            //add meeting to other meetings person
            if (_meetings.Count == 0)
            {
                meeting.Persons.Add(person);
                _meetings.Add(meeting);
                //_dbServiceM.Save(_meetings);
                return;
            }
            foreach (var item in _meetings)
            {
                int? meetingPersonIndex = item.Persons.FindIndex(x => x.Id == person.Id);
                if (meetingPersonIndex != null && meetingPersonIndex != -1)
                {
                    item.Persons.RemoveAt(meetingPersonIndex.Value);
                    item.Persons.Add(person);
                }
            }
            meeting.Persons.Add(person);
            _meetings.Add(meeting);
            Notify?.Invoke(_messagesData.InformationMessages["CreationSuccess"]);
            //_dbServiceM.Save(_meetings);
        }
        public void DeleteMeeting(Meetings meetings, Persons persons, Person person)
        {
            bool isToDelete = false;
            if (meetings.Count > 0)
            {
                _meetingShowData.ShowNamesIndexes(meetings);
                int index = _dataInput.Select(meetings);
                isToDelete = _dataChecking.IsMeetingToDeleteForPerson(meetings, person);

                if (!isToDelete)
                {
                    _uIMessages.WarningMessage(_messagesData.WarningMessages["NoMeetingsToDelete"]);
                    if (_dataInput.Continue())
                        DeleteMeeting(meetings, persons, person);
                    else
                        return;
                }
                else
                {
                    if (!_dataInput.Continue())
                        DeleteMeeting(meetings, persons, person);
                    else
                    {
                        person.PersonMeetings.Remove(meetings[index].Id);
                        persons.ForEach(person => person.PersonMeetings.Remove(meetings[index].Id));
                        meetings.ForEach(x => x.Persons.ForEach(x => x.PersonMeetings.Remove(meetings[index].Id)));
                        meetings.RemoveAt(meetings.FindIndex(x => x.Id == meetings[index].Id));
                        Notify?.Invoke(_messagesData.InformationMessages["DeleteSuccess"]);
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
        public void AddPerson(Meetings meetings, Persons persons)
        {
            if (meetings.Count > 0)
            {
                _meetingShowData.ShowNamesIndexes(meetings);
                int meetingIndex = _dataInput.Select(meetings);

                bool isToAdd = _dataChecking.IsMeetingPersonsListFull(persons, meetings[meetingIndex]);

                if (isToAdd)
                    AddPerson(meetings, persons);
                else
                {
                    List<Person> personsNotAddedYet = SelectNotAddedPersons(meetings[meetingIndex].Id, persons);
                    _meetingShowData.ShowOneItem(meetings[meetingIndex]);
                    _personShowData.ShowNamesIndexesNotAddedYet(persons, personsNotAddedYet);
                    int personIndex = _dataInput.Select(persons);

                    if (!_dataInput.Continue())
                        AddPerson(meetings, persons);
                    else
                    {
                        persons[personIndex].PersonMeetings.Add(meetings[meetingIndex].Id, DateTime.Now);
                        meetings[meetingIndex].Persons.Add(persons[personIndex]);
                        AddMeetingToPersonForechMeeting(meetings, meetings[meetingIndex], persons[personIndex]);
                        Notify?.Invoke(_messagesData.InformationMessages["AddPersonSuccess"]);
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
        static void AddMeetingToPersonForechMeeting(Meetings meetings, Meeting meeting, Person person)
        {
            foreach (var meetingItem in meetings)
            {
                List<Person> personList = new List<Person>();
                if (meetingItem.Id == meeting.Id)
                    continue;
                foreach (var personItem in meetingItem.Persons)
                {
                    if (personItem.Id == person.Id)
                    {
                        personList.Add(person);
                    }
                    else
                    {
                        personList.Add(personItem);
                    }
                }
                meetingItem.Persons.Clear();
                meetingItem.Persons.AddRange(personList);
            }
        }
        public void RemovePerson(Meetings meetings, Persons persons)
        {
            if (meetings.Count > 0)
            {
                _meetingShowData.ShowNamesIndexes(meetings);
                int meetingIndex = _dataInput.Select(meetings);

                _meetingShowData.ShowOneItem(meetings[meetingIndex]);
                int personIndex = _dataInput.Select(meetings[meetingIndex].Persons);

                if (_dataChecking.IsPersonResponsibleForMeeting(meetings[meetingIndex].Persons[personIndex], meetings[meetingIndex]))
                {
                    _uIMessages.WarningMessage(_messagesData.WarningMessages["DeleteResponsiblePersonWarning"]);
                    if (_dataInput.Continue())
                        RemovePerson(meetings, persons);
                    else
                        return;
                }
                else
                {
                    if (!_dataInput.Continue())
                        RemovePerson(meetings, persons);
                    else
                    {
                        Person personToChange = meetings[meetingIndex].Persons[personIndex];
                        RemoveMeetingFromPersonInOtherMeetings(meetings, meetings[meetingIndex], personToChange);
                        RemoveMeetingFromPerson(meetings[meetingIndex], persons, meetings[meetingIndex].Persons[personIndex]);
                        meetings[meetingIndex].Persons.Remove(meetings[meetingIndex].Persons[personIndex]);
                        Notify?.Invoke(_messagesData.InformationMessages["RemovePersonSuccess"]);
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
        static void RemoveMeetingFromPersonInOtherMeetings(Meetings meetings, Meeting meeting, Person person)
        {
            foreach (var itemMeeting in meetings)
            {
                if (itemMeeting.Id == meeting.Id)
                    continue;
                foreach (var itemPerson in itemMeeting.Persons)
                {
                    if (itemPerson.Id == person.Id)
                    {
                        itemPerson.PersonMeetings.Remove(meeting.Id);
                    }
                }
            }
        }
        static void RemoveMeetingFromPerson(Meeting meeting, Persons persons, Person person)
        {
            int index = persons.FindIndex(x => x.Id == person.Id);
            persons[index].PersonMeetings.Remove(meeting.Id);
        }
    }
}
