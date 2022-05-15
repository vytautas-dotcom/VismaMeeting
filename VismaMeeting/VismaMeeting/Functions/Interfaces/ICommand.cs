using VismaMeeting.Employees;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.UI;

namespace VismaMeeting.Functions.Interfaces
{
    internal abstract class ICommand
    {
        internal Person _person;
        internal MeetingList _meetingList;
        internal PersonList _personList;
        internal MeetingSerialazer _meetingSerialazer;
        internal PersonSerialazer _personSerializer;
        internal DataCheck _dataCheck;
        internal PersonMeetingData _personMeetingData;
        internal IShowData<Meeting, MeetingList> _showMeetingData;
        internal IShowData<Person, PersonList> _showPersonData;
        internal FilterShowData _filterShowData;
        internal FilterData _filterData;
        internal ControlPanel _controlPanel;

        public ICommand()
        {
            _meetingSerialazer = new MeetingSerialazer();
            _personSerializer = new PersonSerialazer();
            _meetingList = _meetingSerialazer.Deserialize() ?? new MeetingList();
            _personList = _personSerializer.Deserialize() ?? new PersonList();
            _dataCheck = new DataCheck();
            _showMeetingData = new MeetingShowData();
            _showPersonData = new PersonShowData();
            _personMeetingData = new PersonMeetingData();
            _filterShowData = new FilterShowData(_personList);
            _filterData = new FilterData(_personList);

        }
        public abstract void Execute();
    }
}
