using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.UI;

namespace VismaMeeting_v2.Commands
{
    public class Management
    {
        internal readonly PersonMeetingData _personMeetingData;
        internal readonly IDbService<Persons> _dbServiceP;
        internal readonly IDbService<Meetings> _dbServiceM;
        internal readonly MeetingShowData _meetingShowData;
        internal readonly PersonShowData _personShowData;
        internal readonly DataChecking _dataChecking;
        internal readonly MessagesData _messagesData;
        internal readonly ControlPanel _controlPanel;
        internal readonly CreateUser _createUser;
        internal readonly UIMessages _uIMessages;
        internal readonly DataInput _dataInput;
        public Meetings _meetings;
        public Persons _persons;
        public User User;

        public Management(IDbService<Persons> dbServiceP, IDbService<Meetings> dbServiceM, MeetingShowData meetingShowData, 
                          PersonShowData personShowData, PersonMeetingData personMeetingData, CreateUser createUser,
                          UIMessages uIMessages, DataChecking dataChecking, DataInput dataInput)
        {
            _personMeetingData = personMeetingData;
            _meetingShowData = meetingShowData;
            _personShowData = personShowData;
            _dataChecking = dataChecking;
            _dbServiceP = dbServiceP;
            _dbServiceM = dbServiceM;
            _createUser = createUser;
            _uIMessages = uIMessages;
            _dataInput = dataInput;
            _messagesData = new MessagesData();
            _controlPanel = new ControlPanel();
            
            if (SessionData.User != null)
            {
                User = SessionData.User;
                _meetings = SessionData._meetings;
                _persons = SessionData._persons;
            }
        }
        public void GetAllItems()
        {
            _persons = _dbServiceP.Get();
            SessionData._meetings = _dbServiceM.Get();
            SessionData._persons = _dbServiceP.Get();
        }
        public void CreateUser(bool change = false)
        {
            if (!change)
            {
                GetAllItems();
                SessionData.User = _createUser.SelectUser(_persons);
            }
            else
            {
                GetAllItems();
                SessionData.User = _createUser.SelectUser(_persons);
                Console.Clear();
                _controlPanel.Run();
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
        public void SaveMeetingsAndPersons(Meetings meetings, Persons persons)
        {
            _dbServiceP.Save(persons);
            _dbServiceM.Save(meetings);
        }
        
        public void Exit()
        {
            return;
        }
    }
}
