using VismaMeeting_v2.Models;
using VismaMeeting2.Models;
using VismaMeeting2.UI;

namespace VismaMeeting_v2.Services.DataDisplay
{
    internal class FilterShowData
    {
        public delegate void ExecuteFunction(Meetings meetingList, Persons persons);
        private readonly FilterData _filterData;
        private readonly DataVisualization _dataVisualization;
        private readonly DataCheck _dataCheck;
        private readonly MeetingShowData _meetingShowData;
        ExecuteFunction functionExecuter;
        ControlPanel _controlPanel;
        public FilterShowData(FilterData filterData, DataVisualization dataVisualization, 
                              DataCheck dataCheck, MeetingShowData meetingShowData)
        {
            _filterData = filterData;
            _dataVisualization = dataVisualization;
            _dataCheck = dataCheck;
            _meetingShowData = meetingShowData;
            
        }
        public void ShowFilterParameters()
        {
            string title = "Select parameter to filter the meetings";
            _dataVisualization.DisplayData(title, "", 0, "DarkGray", "DarkYellow", writeTitle: _dataVisualization.WrapedTitle);
            foreach (KeyValuePair<string, int> entry in _filterData.FilterParameters)
            {
                _dataVisualization.DisplayData("", entry.Key, entry.Value, "Gray", "DarkMagenta", writeLine: _dataVisualization.TableLine);
            }
            _dataVisualization.ShowLine(title.Length);
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
            ControlPanel controlPanel;
            _dataVisualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter description text please"));
            _dataVisualization.AskForEntry("Description");
            Meetings meetings = _filterData.FilterByDescription(_dataCheck.GetData(), meetingList);
            if(meetings.Count == 0)
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            _meetingShowData.ShowAllItems(meetings);
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }

        public void MeetingsByResponsiblePerson(Meetings meetingList, Persons persons)
        {
            ControlPanel controlPanel;
            _dataVisualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter name please"));
            _dataVisualization.AskForEntry("Name");
            Meetings meetings = _filterData.FilterByResponsiblePerson(_dataCheck.GetData(), meetingList, persons);
            if (meetings.Count == 0)
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            _meetingShowData.ShowAllItems(meetings);
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }
        public void MeetingsByCategory(Meetings meetingList, Persons persons)
        {
            ControlPanel controlPanel;
            _dataVisualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter category number please"));
            foreach (int i in Enum.GetValues(typeof(MeetCategory)))
            {
                _dataVisualization.DisplayData("", Enum.GetName(typeof(MeetCategory), i), i, "Black", "White", writeLine: _dataVisualization.TableLine);
            }
            _dataVisualization.AskForEntry("Number");
            int index = _dataCheck.GetNumberOfEnum<MeetCategory>();
            Meetings meetings = _filterData.FilterByCategory(index, meetingList);
            if (meetings.Count == 0)
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            _meetingShowData.ShowAllItems(meetings);
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }
        public void MeetingsByType(Meetings meetingList, Persons persons)
        {
            ControlPanel controlPanel;
            _dataVisualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter type number please"));
            foreach (int i in Enum.GetValues(typeof(MeetType)))
            {
                _dataVisualization.DisplayData("", Enum.GetName(typeof(MeetType), i), i, "Black", "White", writeLine: _dataVisualization.TableLine);
            }
            _dataVisualization.AskForEntry("Number");
            int index = _dataCheck.GetNumberOfEnum<MeetType>();
            Meetings meetings = _filterData.FilterByType(index, meetingList);
            if (meetings.Count == 0)
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            _meetingShowData.ShowAllItems(meetings);
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }
        public void MeetingsByDate(Meetings meetingList, Persons persons)
        {
            ControlPanel controlPanel;
            _dataVisualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter date please"));
            _dataVisualization.AskForEntry("Date");
            Meetings meetings = _filterData.FilterByDate(_dataCheck.GetDate(), meetingList);
            if (meetings.Count == 0)
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            _meetingShowData.ShowAllItems(meetings);
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }
        public void MeetingsByNumberOfAttendees(Meetings meetingList, Persons persons)
        {
            ControlPanel controlPanel;
            _dataVisualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter lowest and highest number of users in a meeting please"));
            _dataVisualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("(e.g. (num1,0) - up to num1 including num1"));
            _dataVisualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("(e.g. (num1,num2) - between num1 and num2 including num2"));
            _dataVisualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("(e.g. (0,num2) - over num2"));
            Meetings meetings = _filterData.FilterByNumberOfAttendees(_dataCheck.GetInterval(), meetingList);
            if (meetings.Count == 0)
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("There is no data matching these parameters"));
            }
            _meetingShowData.ShowAllItems(meetings);
            controlPanel = new ControlPanel();
            controlPanel.Run();
        }

    }
}
