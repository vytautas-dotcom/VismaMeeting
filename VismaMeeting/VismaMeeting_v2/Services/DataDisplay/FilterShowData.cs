using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Commands;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.UI;

namespace VismaMeeting_v2.Services.DataDisplay
{
    internal class FilterShowData
    {
        public delegate void ExecuteFunction(Meetings meetingList, Persons persons);
        private readonly MeetingShowData _meetingShowData;
        private readonly MessagesData _messagesData;
        private readonly DataChecking _dataChecking;
        private readonly FilterData _filterData;
        private readonly UIMessages _uIMessages;
        private readonly DataInput _dataInput;
        ExecuteFunction functionExecuter;
        ControlPanel _controlPanel;
        public FilterShowData(FilterData filterData, MeetingShowData meetingShowData, DataInput dataInput, 
                              DataChecking dataChecking, UIMessages uIMessages)
        {
            _filterData = filterData;
            _meetingShowData = meetingShowData;
            _dataInput = dataInput;
            _dataChecking = dataChecking;
            _uIMessages = uIMessages;
            _messagesData = new MessagesData();
            _controlPanel = new ControlPanel();
        }
        public void ShowFilterParameters()
        {
            string title = "Select parameter to filter the meetings";
            _uIMessages.DisplayData(title, "", 0, backgroundColor: "DarkGray", textColor: "DarkYellow",
                userName: SessionData.User.Person.Name, writeTitle: _uIMessages.TableTitle);
            foreach (KeyValuePair<string, int> entry in _messagesData.FilterParameters)
            {
                _uIMessages.DisplayData("", entry.Key, entry.Value, backgroundColor: "Gray", textColor: "DarkMagenta", writeLine: _uIMessages.TableLine);
            }
            _uIMessages.DisplayData("", "", 0, backgroundColor: "DarkGray", textColor: "DarkYellow", 
                showMessage: () => _uIMessages.ShowLine(title.Length, '-'));
        }
        public int AskForIndex()
        {
            int output;
            _dataInput.InputNumber("Number", _messagesData.WarningMessages["InputWarning"], out output);
            if (!_dataChecking.IsSelectedIndexNotOutTheRange(output, _messagesData.FilterParameterIndexes))
            {
                _uIMessages.WarningMessage(_messagesData.WarningMessages["InputWarning"]);
                AskForIndex();
            }
            return output;
        }
        public ExecuteFunction SelectFilter(int index)
        {
            switch (index)
            {
                case 0:
                    return
                    functionExecuter = MeetingsByDescription;
                case 1:
                    return
                    functionExecuter = MeetingsByResponsiblePerson;
                case 2:
                    return
                    functionExecuter = MeetingsByCategory;
                case 3:
                    return
                    functionExecuter = MeetingsByType;
                case 4:
                    return
                    functionExecuter = MeetingsByDate;
                case 5:
                    return
                    functionExecuter = MeetingsByNumberOfAttendees;
                default: return null;
            }
        }
        public void MeetingsByDescription(Meetings meetingList, Persons persons)
        {
            _uIMessages.DisplayData("", "", 0, backgroundColor: "White", textColor: "Black", showMessage: () => Console.WriteLine("Enter description text please"));
            _uIMessages.InputInformationMessage("Description");
            Meetings meetings = _filterData.FilterByDescription(_dataInput.Input(), meetingList);
            if (meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.DisplayData(_messagesData.InformationMessages["NoMatch"]);
            }
            _meetingShowData.ShowAllItems(meetings);
            _controlPanel.Run();
        }
        public void MeetingsByResponsiblePerson(Meetings meetingList, Persons persons)
        {
            _uIMessages.DisplayData("", "", 0, backgroundColor: "White", textColor: "Black", showMessage: () => Console.WriteLine("Enter name please"));
            _uIMessages.InputInformationMessage("Name");
            Meetings meetings = _filterData.FilterByResponsiblePerson(_dataInput.Input(), meetingList, persons);
            if (meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.DisplayData(_messagesData.InformationMessages["NoMatch"]);
            }
            _meetingShowData.ShowAllItems(meetings);
            _controlPanel.Run();
        }
        public void MeetingsByCategory(Meetings meetingList, Persons persons)
        {
            _uIMessages.DisplayData("", "", 0, backgroundColor: "White", textColor: "Black", showMessage: () => Console.WriteLine("Enter category number please"));
            foreach (int i in Enum.GetValues(typeof(MeetCategory)))
            {
                _uIMessages.DisplayData("", Enum.GetName(typeof(MeetCategory), i), i, backgroundColor: "Black", textColor: "White", writeLine: _uIMessages.TableLine);
            }
            int output;
            _dataInput.EnumInput<MeetCategory>("Number", _messagesData.WarningMessages["InputWarning"], out output);
            Meetings meetings = _filterData.FilterByCategory(output, meetingList);
            if (meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.DisplayData("", "", 0, backgroundColor: "Black", textColor: "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            _meetingShowData.ShowAllItems(meetings);
            _controlPanel.Run();
        }
        public void MeetingsByType(Meetings meetingList, Persons persons)
        {
            _uIMessages.DisplayData("", "", 0, backgroundColor: "White", textColor: "Black", showMessage: () => Console.WriteLine("Enter type number please"));
            foreach (int i in Enum.GetValues(typeof(MeetType)))
            {
                _uIMessages.DisplayData("", Enum.GetName(typeof(MeetType), i), i, backgroundColor: "Black", textColor: "White", writeLine: _uIMessages.TableLine);
            }
            int output;
            _dataInput.EnumInput<MeetType>("Number", _messagesData.WarningMessages["InputWarning"], out output);
            Meetings meetings = _filterData.FilterByCategory(output, meetingList);
            if (meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.DisplayData("", "", 0, backgroundColor: "Black", textColor: "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            _meetingShowData.ShowAllItems(meetings);
            _controlPanel.Run();
        }
        public void MeetingsByDate(Meetings meetingList, Persons persons)
        {
            _uIMessages.DisplayData("", "", 0, backgroundColor: "White", textColor: "Black", showMessage: () => Console.WriteLine("Enter date please"));
            DateTime output;
            _dataInput.InputDate("Date", _messagesData.WarningMessages["InputWarning"], out output);
            Meetings meetings = _filterData.FilterByDate(output, meetingList);
            if (meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.DisplayData("", "", 0, backgroundColor: "Black", textColor: "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            _meetingShowData.ShowAllItems(meetings);
            _controlPanel.Run();
        }
        public void MeetingsByNumberOfAttendees(Meetings meetingList, Persons persons)
        {
            _uIMessages.DisplayData("", "", 0, backgroundColor: "White", textColor: "Black", showMessage: () => Console.WriteLine("Enter lowest and highest number of users in a meeting please"));
            _uIMessages.DisplayData("", "", 0, backgroundColor: "Black", textColor: "White", showMessage: () => Console.WriteLine("(e.g. (num1,0)    - up to num1 including num1"));
            _uIMessages.DisplayData("", "", 0, backgroundColor: "Black", textColor: "White", showMessage: () => Console.WriteLine("(e.g. (num1,num2) - between num1 and num2 including num2"));
            _uIMessages.DisplayData("", "", 0, backgroundColor: "Black", textColor: "White", showMessage: () => Console.WriteLine("(e.g. (0,num2)    - over num2"));
            Meetings meetings = _filterData.FilterByNumberOfAttendees(_dataInput.GetInterval(), meetingList);
            if (meetings.Count == 0)
            {
                Console.Clear();
                _uIMessages.DisplayData("", "", 0, backgroundColor: "Black", textColor: "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            Console.Clear();
            _meetingShowData.ShowAllItems(meetings);
            _controlPanel.Run();
        }
    }
}
