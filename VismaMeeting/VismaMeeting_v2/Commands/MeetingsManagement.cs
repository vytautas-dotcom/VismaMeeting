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
    public class MeetingsManagement : Management
    {
        public MeetingsManagement(IDbService<Meetings> dbServiceM, IDbService<Persons> dbServiceP, MeetingShowData meetingShowData,
                                  PersonShowData personShowData, PersonMeetingData personMeetingData, CreateUser createUser,
                                  UIMessages uIMessages, DataChecking dataChecking, DataInput dataInput) :
            base(dbServiceP, dbServiceM, meetingShowData, personShowData, personMeetingData, createUser,
                 uIMessages, dataChecking, dataInput)
        {
        }
        public void CreateMeeting()
        {
            Meeting meeting = _personMeetingData.CreateMeeting(User.Person);
            _personMeetingData.Notify += _uIMessages.ActionInformation;
            _personMeetingData.SaveCreatedMeeting(_meetings, meeting, _persons, User.Person);
            SaveMeetingsAndPersons(_meetings, _persons);
            Console.Clear();
            _controlPanel.Run();
        }
        public void DeleteMeeting()
        {
            if (!_dataChecking.IsMeetingToDeleteForPerson(_meetings, User.Person) || _meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.WarningMessage(_messagesData.WarningMessages["NoMeetingsToDelete"]);
                _controlPanel.Run();
                return;
            }
            _personMeetingData.Notify += _uIMessages.ActionInformation;
            _personMeetingData.DeleteMeeting(_meetings, _persons, User.Person);
            SaveMeetingsAndPersons(_meetings, _persons);
            _controlPanel.Run();
        }
    }
}
