using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.UI;

namespace VismaMeeting_v2.Commands
{
    public class Management
    {
        public User User { get; set; }
        public Meetings _meetings;
        public Persons _persons;
        internal ControlPanel controlPanel;
        internal readonly IDbService<Persons> _dbServiceP;
        internal readonly IDbService<Meetings> _dbServiceM;
        internal readonly DataCheck _dataCheck;
        internal readonly DataVisualization _dataVisualization;
        internal readonly MeetingShowData _meetingShowData;
        internal readonly PersonShowData _personShowData;
        internal readonly PersonMeetingData _personMeetingData;
        internal readonly CreateUser _createUser;
        internal readonly MessagesData _messagesData;
        //
        internal readonly UIMessages _uIMessages;
        internal readonly DataChecking _dataChecking;
        internal readonly DataInput _dataInput;

        public Management(IDbService<Persons> dbServiceP, IDbService<Meetings> dbServiceM, DataCheck dataCheck,
                          DataVisualization dataVisualization, MeetingShowData meetingShowData, PersonShowData personShowData,
                          PersonMeetingData personMeetingData, CreateUser createUser,
                          UIMessages uIMessages, DataChecking dataChecking, DataInput dataInput, MessagesData messagesData)
        {
            _dbServiceP = dbServiceP;
            _dbServiceM = dbServiceM;
            _dataCheck = dataCheck;
            _dataVisualization = dataVisualization;
            _meetingShowData = meetingShowData;
            _personShowData = personShowData;
            _personMeetingData = personMeetingData;
            _createUser = createUser;
            //
            _uIMessages = uIMessages;
            _dataChecking = dataChecking;
            _dataInput = dataInput;
            _messagesData = messagesData;
            controlPanel = new ControlPanel();
            //
            if (IManagement.User != null)
            {
                User = IManagement.User;
                _meetings = IManagement._meetings;
                _persons = IManagement._persons;
            }
        }
        public void GetAllItems()
        {
            _persons = _dbServiceP.Get();
            IManagement._meetings = _dbServiceM.Get();
            IManagement._persons = _dbServiceP.Get();
        }
        public void CreateUser(bool change = false)
        {
            if (!change)
            {
                GetAllItems();
                IManagement.User = _createUser.SelectUser(_persons);
            }
            else
            {
                GetAllItems();
                IManagement.User = _createUser.SelectUser(_persons);
                Console.Clear();
                controlPanel.Run();
            }
        }

        public void SaveMeetings(Meetings meetings)
        {
            _dbServiceM.Save(meetings);
        }
        public void SavePersons(Persons persons)
        {
            _dbServiceP.Save(persons);
        }

        public void SaveMeetingPerson(Meeting meeting, Person person)
        {
            //add meeting to person
            int personIndex = _persons.FindIndex(x => x.Id == person.Id);
            _persons.RemoveAt(personIndex);
            _persons.Add(person);
            _dbServiceP.Save(_persons);

            //add meeting to other meetings person
            if (_meetings.Count == 0)
            {
                meeting.Persons.Add(person);
                _meetings.Add(meeting);
                _dbServiceM.Save(_meetings);
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
            _dbServiceM.Save(_meetings);
        }
        public void Exit()
        {
            return;
        }
    }
}
