using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class DeleteMeeting : ICommand
    {
        public DeleteMeeting(Person person)
            => _person = person;
        public override void Execute()
        {
            if(!_dataCheck.IsMeetingToDeleteForPerson(_meetingList, _person))
            {
                _controlPanel = new ControlPanel(consoleClear: true);
                _controlPanel.RunProgram();
            }
            _showMeetingData.ShowNamesIndexes(_meetingList);
            int index = _dataCheck.SelectMeetigForDelete(_meetingList, _person);
            if (!_dataCheck.Confirm())
            {
                _controlPanel = new ControlPanel(consoleClear: true);
                _controlPanel.RunProgram();
            }
            else
            {
                PersonList personList = _personSerializer.Deserialize();
                _personMeetingData.RemoveMeetingFromPersonMeetings(_meetingList[index].Id, personList);
                _personSerializer.JsonSerialize(personList);
                _meetingList.RemoveAt(index);
                _meetingSerialazer.JsonSerialize(_meetingList);
            }
            _controlPanel = new ControlPanel(consoleClear: true);
            _controlPanel.RunProgram();
        }
    }
}
