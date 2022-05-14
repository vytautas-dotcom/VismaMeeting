using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class DeleteMeeting : ICommand
    {
        private readonly Person _person;
        private readonly MeetingList _meetingList;
        private readonly MeetingSerialazer _meetingSerialazer;
        private readonly PersonSerialazer _personSerializer;
        private readonly DataCheck _dataCheck;
        private readonly PersonMeetingData _personMeetingData;
        private readonly IShowData<Meeting, MeetingList> _showMeetingData;

        public DeleteMeeting(Person person, MeetingList meetingList, MeetingSerialazer meetingSerialazer,
                             PersonSerialazer personSerialazer, DataCheck dataCheck, IShowData<Meeting, 
                                 MeetingList> showMeetingData, PersonMeetingData personMeetingData)
        {
            _person = person;
            _meetingSerialazer = meetingSerialazer;
            _personSerializer = personSerialazer;
            _meetingList = _meetingSerialazer.Deserialize() ?? meetingList;
            _dataCheck = dataCheck;
            _showMeetingData = showMeetingData;
            _personMeetingData = personMeetingData;
        }
        public void Create()
        {
            if(!_dataCheck.IsMeetingToDeleteForPerson(_meetingList, _person))
            {
                new ControlPanel();
            }
            _showMeetingData.ShowNamesIndexes(_meetingList);
            int index = _dataCheck.SelectMeetigForDelete(_meetingList, _person);
            if (!_dataCheck.Confirm())
            {
                new ControlPanel();
            }
            else
            {
                PersonList personList = _personSerializer.Deserialize();
                _personMeetingData.RemoveMeetingFromPersonMeetings(_meetingList[index].Id, personList);
                _personSerializer.JsonSerialize(personList);
                _meetingList.RemoveAt(index);
                _meetingSerialazer.JsonSerialize(_meetingList);
                new ControlPanel();
            }
        }
    }
}
