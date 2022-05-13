using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;

namespace VismaMeeting.Functions
{
    internal class DeleteMeeting : ICommand
    {
        private readonly Person _person;
        private readonly MeetingList _meetingList;
        private readonly MeetingSerialazer _meetingSerialazer;
        private readonly DataCheck _dataCheck;
        private readonly IShowData<Meeting, MeetingList> _showMeetingData;

        public DeleteMeeting(Person person, MeetingList meetingList, MeetingSerialazer meetingSerialazer,
                             DataCheck dataCheck, IShowData<Meeting, MeetingList> showMeetingData)
        {
            _person = person;
            _meetingSerialazer = meetingSerialazer;
            _meetingList = _meetingSerialazer.Deserialize() ?? meetingList;
            _dataCheck = dataCheck;
            _showMeetingData = showMeetingData;
        }
        public void Create()
        {
            ShowMeetsList();
            int? index = _dataCheck.SelectMeetig(_meetingList, _person);
            if (index == null)
                Create();
            _meetingList.RemoveAt(index.Value);
            _meetingSerialazer.JsonSerialize(_meetingList);
        }
        public void ShowMeetsList()
        {
            _showMeetingData.ShowNamesIndexes(_meetingList);
        }
    }
}
