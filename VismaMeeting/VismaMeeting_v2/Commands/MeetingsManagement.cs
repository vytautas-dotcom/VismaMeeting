using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
using VismaMeeting_v2.UI;

namespace VismaMeeting_v2.Commands
{
    public class MeetingsManagement : Management
    {
        public MeetingsManagement(IDbService<Meetings> dbServiceM, IDbService<Persons> dbServiceP,
                                  DataCheck dataCheck, DataVisualization dataVisualization, MeetingShowData meetingShowData,
                                  PersonShowData personShowData, PersonMeetingData personMeetingData, CreateUser createUser) :
            base(dbServiceP, dbServiceM, dataCheck, dataVisualization, meetingShowData, personShowData, personMeetingData, createUser)
        {
        }
        #region Create Meeting
        public void Create()
        {
            CreateUser();
            Meeting meeting = _dataCheck.CreateMeeting(User.Person, _personMeetingData);
            _meetings.Add(meeting);
            SaveMeetings(_meetings);
            _persons.RemoveAt(_persons.FindIndex(x => x.Id == User.Person.Id));
            _persons.Add(User.Person);
            SavePersons(_persons);
            Console.Clear();
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }
        #endregion
        #region Delete Meeting
        public void DeleteMeeting()
        {
            CreateUser();
            if (!_dataCheck.IsMeetingToDeleteForPerson(_meetings, User.Person))
            {
                Console.Clear();
                controlPanel = new ControlPanel();
                controlPanel.Run();
            }
            _meetingShowData.ShowNamesIndexes(_meetings);
            int index = _dataCheck.SelectMeetigForDelete(_meetings, User.Person);
            _personMeetingData.RemoveMeetingFromPersonMeetings(_meetings[index].Id, _persons);
            SavePersons(_persons);
            _meetings.RemoveAt(index);
            SaveMeetings(_meetings);

            Console.Clear();
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }
        #endregion
        
    }
}
