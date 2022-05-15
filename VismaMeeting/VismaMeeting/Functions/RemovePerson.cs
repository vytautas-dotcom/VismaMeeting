using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class RemovePerson : ICommand
    {
        public override void Execute()
        {
            if (_meetingList.Count == 0)
            {
                _controlPanel = new ControlPanel(consoleClear: true);
                _controlPanel.RunProgram();
            }
            _showMeetingData.ShowNamesIndexes(_meetingList);
            int indexMeeting = _dataCheck.Select(_meetingList);
            if (_meetingList[indexMeeting].Persons.Count == 0)
            {
                _controlPanel = new ControlPanel(consoleClear: true);
                _controlPanel.RunProgram();
            }
            _showMeetingData.ShowOneItem(_meetingList[indexMeeting]);
            
            int indexPerson = _dataCheck.SelectPersonToRemoveFromMeeting(_meetingList[indexMeeting]);
            if (!_dataCheck.Confirm())
            {
                _controlPanel = new ControlPanel(consoleClear: true);
                _controlPanel.RunProgram();
            }
            else
            {
                _personMeetingData.RemoveMeetingFromPersonMeetings(_meetingList[indexMeeting].Id, _personList[indexPerson]);
                _personSerializer.JsonSerialize(_personList);
                _personMeetingData.RemovePersonFromMeeting(_personList[indexPerson].Id, _meetingList[indexMeeting]);
                _meetingSerialazer.JsonSerialize(_meetingList);
                _controlPanel = new ControlPanel(consoleClear: true);
                _controlPanel.RunProgram();
            }
        }
    }
}
