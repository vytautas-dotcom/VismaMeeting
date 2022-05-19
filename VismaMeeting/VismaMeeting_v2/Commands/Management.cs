using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
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

        public Management(IDbService<Persons> dbServiceP, IDbService<Meetings> dbServiceM, DataCheck dataCheck,
                          DataVisualization dataVisualization, MeetingShowData meetingShowData, PersonShowData personShowData,
                          PersonMeetingData personMeetingData, CreateUser createUser)
        {
            _meetings = new();
            _persons = new();
            _dbServiceP = dbServiceP;
            _dbServiceM = dbServiceM;
            _dataCheck = dataCheck;
            _dataVisualization = dataVisualization;
            _meetingShowData = meetingShowData;
            _personShowData = personShowData;
            _personMeetingData = personMeetingData;
            _createUser = createUser;
        }
        public void GetAllItems()
        {
            _meetings = _dbServiceM.Get();
            _persons = _dbServiceP.Get();
        }
        public void CreateUser(bool change = false)
        {
            if (!change)
            {
                GetAllItems();
                User = _createUser.SelectUser(_persons);
            }
            else
            {
                GetAllItems();
                User = _createUser.SelectUser(_persons);
                Console.Clear();
                controlPanel = new ControlPanel();
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
    }
}
