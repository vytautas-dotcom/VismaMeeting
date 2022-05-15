using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class AddPerson : ICommand
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
            if (_meetingList[indexMeeting].Persons.Count == _personList.Count)
            {
                _controlPanel = new ControlPanel(consoleClear: true);
                _controlPanel.RunProgram();
            }
            _showMeetingData.ShowOneItem(_meetingList[indexMeeting]);
            ((PersonShowData)_showPersonData).ShowNamesIndexesNotAddedYet(_personList, _meetingList[indexMeeting].Persons);
            int indexPerson = _dataCheck.SelectPersonForMeeting(_meetingList[indexMeeting], _personList);
            if (!_dataCheck.Confirm())
            {
                _controlPanel = new ControlPanel(consoleClear: true);
                _controlPanel.RunProgram();
            }
            else
            {
                _personMeetingData.AddMeetingToPerson(_meetingList[indexMeeting].Id, _personList[indexPerson]);
                _personSerializer.JsonSerialize(_personList);
                _personMeetingData.AddPersonToMeeting(_meetingList[indexMeeting], _personList[indexPerson]);
                _meetingSerialazer.JsonSerialize(_meetingList);
                _controlPanel = new ControlPanel(consoleClear: true);
                _controlPanel.RunProgram();
            }
        }
    }
}
