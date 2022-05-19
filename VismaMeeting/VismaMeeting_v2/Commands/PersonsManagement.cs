using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
using VismaMeeting_v2.UI;

namespace VismaMeeting_v2.Commands
{
    public class PersonsManagement : Management
    {
        public PersonsManagement(IDbService<Persons> dbServiceP, IDbService<Meetings> dbServiceM,
                                 DataCheck dataCheck, DataVisualization dataVisualization, MeetingShowData meetingShowData, 
                                 PersonShowData personShowData, PersonMeetingData personMeetingData, CreateUser createUser) : 
            base(dbServiceP, dbServiceM, dataCheck, dataVisualization, meetingShowData, personShowData, personMeetingData, createUser)
        {
        }
        #region Add Person
        public void AddPerson()
        {
            CreateUser();
            if (_meetings.Count == 0)
            {
                Console.Clear();
                controlPanel = new ControlPanel();
                controlPanel.Run();
            }
            _meetingShowData.ShowNamesIndexes(_meetings);
            int indexMeeting = _dataCheck.Select(_meetings);
            if (_meetings[indexMeeting].Persons.Count == _persons.Count)
            {
                Console.Clear();
                controlPanel = new ControlPanel();
                controlPanel.Run();
            }
            _meetingShowData.ShowOneItem(_meetings[indexMeeting]);
            ((PersonShowData)_personShowData).ShowNamesIndexesNotAddedYet(_persons, _meetings[indexMeeting].Persons);
            int indexPerson = _dataCheck.SelectPersonForMeeting(_meetings[indexMeeting], _persons);
            if (!_dataCheck.Confirm())
            {
                Console.Clear();
                controlPanel = new ControlPanel();
                controlPanel.Run();
            }
            else
            {
                _personMeetingData.AddMeetingToPerson(_meetings[indexMeeting].Id, _persons[indexPerson]);
                SavePersons(_persons);
                _personMeetingData.AddPersonToMeeting(_meetings[indexMeeting], _persons[indexPerson]);
                SaveMeetings(_meetings);

            }
            Console.Clear();
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }
        #endregion
        #region Remove Person
        public void RemovePerson()
        {
            CreateUser();
            if (_meetings.Count == 0)
            {
                Console.Clear();
                controlPanel = new ControlPanel();
                controlPanel.Run();
            }
            _meetingShowData.ShowNamesIndexes(_meetings);
            int indexMeeting = _dataCheck.Select(_meetings);
            if (_meetings[indexMeeting].Persons.Count == 0)
            {
                Console.Clear();
                controlPanel = new ControlPanel();
                controlPanel.Run();
            }
            _meetingShowData.ShowOneItem(_meetings[indexMeeting]);

            int indexPerson = _dataCheck.SelectPersonToRemoveFromMeeting(_meetings[indexMeeting]);
            if (!_dataCheck.Confirm())
            {
                Console.Clear();
                controlPanel = new ControlPanel();
                controlPanel.Run();
            }
            else
            {
                _personMeetingData.RemoveMeetingFromPersonMeetings(_meetings[indexMeeting].Id, _persons[indexPerson]);
                SavePersons(_persons);
                _personMeetingData.RemovePersonFromMeeting(_persons[indexPerson].Id, _meetings[indexMeeting]);
                SaveMeetings(_meetings);
            }
            Console.Clear();
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }
        #endregion
        

    }
}
