using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;

namespace VismaMeeting.Functions
{
    internal class FilterMeeting : ICommand
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
        private readonly FilterShowData _filterShowData;
        private readonly FilterData _filterData;

        public FilterMeeting(Person person, MeetingList meetingList, PersonList personList, MeetingSerialazer meetingSerialazer,
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
            _filterShowData = new FilterShowData(_personList);
            _filterData = new FilterData(_personList);

        }
        public override void Execute()
        {
            _filterShowData.ShowFilterParameters();
            int index = _dataCheck.Select(_filterData.FilterParameterIndexes);
            var function = _filterShowData.SelectFilter(index);
            function?.Invoke(_meetingList);
        }
    }
}
