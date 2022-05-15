using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class CreateMeeting : ICommand
    {
        public CreateMeeting(Person person)
            => _person = person;
        public override void Execute()
        {
            Meeting meeting = _dataCheck.CreateMeeting(_person, _personMeetingData);
            _meetingList.Add(meeting);
            _meetingSerialazer.JsonSerialize(_meetingList);
            PersonList personList = _personSerializer.Deserialize();
            personList.RemoveAt(personList.FindIndex(x => x.Id == _person.Id));
            personList.Add(_person);
            _personSerializer.JsonSerialize(personList);
            _controlPanel = new ControlPanel(consoleClear: true);
            _controlPanel.RunProgram();
        }
    }
}
