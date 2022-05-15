using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class CreateMeeting : ICommand
    {
        private readonly Person _person;
        private readonly MeetingList _meetingList;
        private readonly MeetingSerialazer _meetingSerialazer;
        private readonly PersonSerialazer _personSerialazer;
        private readonly DataCheck _dataCheck;
        private readonly PersonMeetingData _personMeetingData;
        ControlPanel controlPanel;

        public CreateMeeting(Person person, MeetingList meetingList, MeetingSerialazer meetingSerialazer,
                             PersonSerialazer personSerialazer, DataCheck dataCheck, PersonMeetingData personMeetingData)
        {
            _person = person;
            _meetingSerialazer = meetingSerialazer;
            _personSerialazer = personSerialazer;
            _meetingList = _meetingSerialazer.Deserialize() ?? meetingList;
            _dataCheck = dataCheck;
            _personMeetingData = personMeetingData;

        }
        public override void Execute()
        {
            Meeting meeting = _dataCheck.CreateMeeting(_person, _personMeetingData);
            _meetingList.Add(meeting);
            _meetingSerialazer.JsonSerialize(_meetingList);
            PersonList personList = _personSerialazer.Deserialize();
            personList.RemoveAt(personList.FindIndex(x => x.Id == _person.Id));
            personList.Add(_person);
            _personSerialazer.JsonSerialize(personList);
            controlPanel = new ControlPanel(consoleClear: true);
            controlPanel.RunProgram();
        }
    }
}
