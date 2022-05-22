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
                                 DataVisualization dataVisualization, MeetingShowData meetingShowData,
                                 PersonShowData personShowData, PersonMeetingData personMeetingData, CreateUser createUser,
                                 UIMessages uIMessages, DataChecking dataChecking, DataInput dataInput, MessagesData messagesData) :
            base(dbServiceP, dbServiceM, dataVisualization, meetingShowData, personShowData, personMeetingData, createUser,
                uIMessages, dataChecking, dataInput, messagesData)
        {
        }
        public void AddPerson()
        {
            if (_meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.WarningMessage(_messagesData.WarningMessages["NoMeetingsToAdd"]);
                controlPanel.Run();
                return;
            }
            _personMeetingData.AddPerson(_meetings, _persons);
            SaveMeetings(_meetings);
            SavePersons(_persons);
            controlPanel.Run();
        }
        public void RemovePerson()
        {
            if (_meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.WarningMessage(_messagesData.WarningMessages["NoMeetingCreated"]);
                controlPanel.Run();
                return;
            }
            _personMeetingData.RemovePerson(_meetings, _persons);
            SaveMeetings(_meetings);
            SavePersons(_persons);
            controlPanel.Run();
            return;

        }
    }
}
