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
    public class PersonsManagement : Management
    {
        public PersonsManagement(IDbService<Persons> dbServiceP, IDbService<Meetings> dbServiceM, MeetingShowData meetingShowData,
                                 PersonShowData personShowData, PersonMeetingData personMeetingData, CreateUser createUser,
                                 UIMessages uIMessages, DataChecking dataChecking, DataInput dataInput) :
            base(dbServiceP, dbServiceM, meetingShowData, personShowData, personMeetingData, createUser,
                 uIMessages, dataChecking, dataInput)
        {
        }
        public void AddPerson()
        {
            if (_meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.WarningMessage(_messagesData.WarningMessages["NoMeetingsToAdd"]);
                _controlPanel.Run();
                return;
            }
            _personMeetingData.Notify += _uIMessages.ActionInformation;
            _personMeetingData.AddPerson(_meetings, _persons);
            SaveMeetingsAndPersons(_meetings, _persons);
            _controlPanel.Run();
        }
        public void RemovePerson()
        {
            if (_meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.WarningMessage(_messagesData.WarningMessages["NoMeetingCreated"]);
                _controlPanel.Run();
                return;
            }
            _personMeetingData.Notify += _uIMessages.ActionInformation;
            _personMeetingData.RemovePerson(_meetings, _persons);
            SaveMeetingsAndPersons(_meetings, _persons);
            _controlPanel.Run();
        }
    }
}
