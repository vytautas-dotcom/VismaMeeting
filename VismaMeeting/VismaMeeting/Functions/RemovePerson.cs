using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class RemovePerson : ICommand
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

        public RemovePerson(Person person, MeetingList meetingList, PersonList personList, MeetingSerialazer meetingSerialazer,
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
        public void Create()
        {
            if (_meetingList.Count == 0)
            {
                new ControlPanel();
            }
            _showMeetingData.ShowNamesIndexes(_meetingList);
            int indexMeeting = _dataCheck.Select(_meetingList);
            if (_meetingList[indexMeeting].Persons.Count == 0)
            {
                new ControlPanel();
            }
            _showMeetingData.ShowOneItem(_meetingList[indexMeeting]);
            
            int indexPerson = _dataCheck.SelectPersonToRemoveFromMeeting(_meetingList[indexMeeting]);
            if (!_dataCheck.Confirm())
            {
                new ControlPanel();
            }
            else
            {
                _personMeetingData.RemoveMeetingFromPersonMeetings(_meetingList[indexMeeting].Id, _personList[indexPerson]);
                _personSerializer.JsonSerialize(_personList);
                _personMeetingData.RemovePersonFromMeeting(_personList[indexPerson].Id, _meetingList[indexMeeting]);
                _meetingSerialazer.JsonSerialize(_meetingList);
            }
            new ControlPanel();
        }
    }
}
