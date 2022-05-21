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
    public class PersonsManagement : Management
    {
        public PersonsManagement(IDbService<Persons> dbServiceP, IDbService<Meetings> dbServiceM,
                                 DataCheck dataCheck, DataVisualization dataVisualization, MeetingShowData meetingShowData, 
                                 PersonShowData personShowData, PersonMeetingData personMeetingData, CreateUser createUser,
                                 UIMessages uIMessages, DataChecking dataChecking, DataInput dataInput, MessagesData messagesData) : 
            base(dbServiceP, dbServiceM, dataCheck, dataVisualization, meetingShowData, personShowData, personMeetingData, createUser,
                uIMessages, dataChecking, dataInput, messagesData)
        {
        }
        #region Add Person
        public void AddPerson()
        {
            if (_meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.WarningMessage(_messagesData.WarningMessages["NoMeetingsToAdd"]);
                controlPanel.Run();
            }
            else
            {
                _personMeetingData.AddPerson(_meetings, _persons);
                SaveMeetings(_meetings);
                SavePersons(_persons);
                controlPanel.Run();
            }
        }
        #endregion
        #region Remove Person
        public void RemovePerson()
        {
            //CreateUser();
            //if (_meetings.Count == 0)
            //{
            //    Console.Clear();
            //    controlPanel = new ControlPanel();
            //    controlPanel.Run();
            //}
            //_meetingShowData.ShowNamesIndexes(_meetings);
            //int indexMeeting = _dataCheck.Select(_meetings);
            //if (_meetings[indexMeeting].Persons.Count == 0)
            //{
            //    Console.Clear();
            //    controlPanel = new ControlPanel();
            //    controlPanel.Run();
            //}
            //_meetingShowData.ShowOneItem(_meetings[indexMeeting]);

            //int indexPerson = _dataCheck.SelectPersonToRemoveFromMeeting(_meetings[indexMeeting]);
            //if (!_dataCheck.Confirm())
            //{
            //    Console.Clear();
            //    controlPanel = new ControlPanel();
            //    controlPanel.Run();
            //}
            //else
            //{
            //    _personMeetingData.RemoveMeetingFromPersonMeetings(_meetings[indexMeeting].Id, _persons[indexPerson]);
            //    SavePersons(_persons);
            //    _personMeetingData.RemovePersonFromMeeting(_persons[indexPerson].Id, _meetings[indexMeeting]);
            //    SaveMeetings(_meetings);
            //}
            //Console.Clear();
            //controlPanel = new ControlPanel();
            //controlPanel.Run();
        }
        #endregion
        

    }
}
