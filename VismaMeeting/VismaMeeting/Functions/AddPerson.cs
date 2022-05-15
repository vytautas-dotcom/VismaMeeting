using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class AddPerson : ICommand
    {
        private readonly Person _person;
        private MeetingList _meetingList;
        private PersonList _personList;
        private readonly MeetingSerialazer _meetingSerialazer;
        private readonly PersonSerialazer _personSerializer;
        private readonly DataCheck _dataCheck;
        private readonly PersonMeetingData _personMeetingData;
        private readonly IShowData<Meeting, MeetingList> _showMeetingData;
        private readonly IShowData<Person, PersonList> _showPersonData;
        ControlPanel controlPanel;
        public AddPerson(Person person, MeetingList meetingList, PersonList personList, MeetingSerialazer meetingSerialazer,
                             PersonSerialazer personSerialazer, DataCheck dataCheck, IShowData<Meeting,
                                 MeetingList> showMeetingData, IShowData<Person, PersonList> showPersonData,
                             PersonMeetingData personMeetingData)
        {
            _person = person;
            _meetingSerialazer = meetingSerialazer;
            _personSerializer = personSerialazer;
            _meetingList = _meetingSerialazer.Deserialize() ?? meetingList;
            _personList = _personSerializer.Deserialize() ?? personList;
            _dataCheck = dataCheck;
            _showMeetingData = showMeetingData;
            _showPersonData = showPersonData;
            _personMeetingData = personMeetingData;

        }
        public override void Execute()
        {
            if (_meetingList.Count == 0)
            {
                controlPanel = new ControlPanel(consoleClear: true);
                controlPanel.RunProgram();
            }
            _showMeetingData.ShowNamesIndexes(_meetingList);
            int indexMeeting = _dataCheck.Select(_meetingList);
            if (_meetingList[indexMeeting].Persons.Count == _personList.Count)
            {
                controlPanel = new ControlPanel(consoleClear: true);
                controlPanel.RunProgram();
            }
            _showMeetingData.ShowOneItem(_meetingList[indexMeeting]);
            ((PersonShowData)_showPersonData).ShowNamesIndexesNotAddedYet(_personList, _meetingList[indexMeeting].Persons);
            int indexPerson = _dataCheck.SelectPersonForMeeting(_meetingList[indexMeeting], _personList);
            if (!_dataCheck.Confirm())
            {
                controlPanel = new ControlPanel(consoleClear: true);
                controlPanel.RunProgram();
            }
            else
            {
                _personMeetingData.AddMeetingToPerson(_meetingList[indexMeeting].Id, _personList[indexPerson]);
                _personSerializer.JsonSerialize(_personList);
                _personMeetingData.AddPersonToMeeting(_meetingList[indexMeeting], _personList[indexPerson]);
                _meetingSerialazer.JsonSerialize(_meetingList);
                controlPanel = new ControlPanel(consoleClear: true);
                controlPanel.RunProgram();
            }
        }
    }
}
