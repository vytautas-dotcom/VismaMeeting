using VismaMeeting.Employees;
using VismaMeeting.MeetingData;

namespace VismaMeeting.Functions.Interfaces
{
    internal class FilterShowData
    {
        public delegate void ExecuteFunction(MeetingList meetingList);
        private readonly FilterData _filterData;
        private readonly DataVisualization _dataVizualization;
        private readonly DataCheck _dataCheck;
        private readonly MeetingShowData _meetingShowData;
        private readonly PersonList _personList;
        ExecuteFunction functionExecuter;
        public FilterShowData(PersonList personList)
        {
            _personList = personList;
            _filterData = new FilterData(_personList);
            _dataVizualization = new DataVisualization();
            _dataCheck = new DataCheck();
            _meetingShowData = new MeetingShowData();
            
        }
        public void ShowFilterParameters()
        {
            string title = "Select parameter to filter the meetings";
            _dataVizualization.DisplayData(title, "", 0, "DarkGray", "DarkYellow", writeTitle: _dataVizualization.WrapedTitle);
            foreach (KeyValuePair<string, int> entry in _filterData.FilterParameters)
            {
                _dataVizualization.DisplayData("", entry.Key, entry.Value, "Gray", "DarkMagenta", writeLine: _dataVizualization.TableLine);
            }
            _dataVizualization.ShowLine(title.Length);
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
        public void MeetingsByDescription(MeetingList meetingList)
        {
            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter text description please"));
            _dataVizualization.AskForEntry("Description");
            MeetingList meetings = _filterData.FilterByDescription(_dataCheck.GetData(), meetingList);
            _meetingShowData.ShowAllItems(meetings);
            return;
        }

        public void MeetingsByResponsiblePerson(MeetingList meetingList)
        {
            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter name please"));
            _dataVizualization.AskForEntry("Name");
            MeetingList meetings = _filterData.FilterByResponsiblePerson(_dataCheck.GetData(), meetingList);
            _meetingShowData.ShowAllItems(meetings);
            return;
        }
        public void MeetingsByCategory(MeetingList meetingList)
        {

            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter category number please"));
            foreach (int i in Enum.GetValues(typeof(MeetCategory)))
            {
                _dataVizualization.DisplayData("", Enum.GetName(typeof(MeetCategory), i), i, "Black", "White", writeLine: _dataVizualization.TableLine);
            }
            _dataVizualization.AskForEntry("Number");
            int index = _dataCheck.GetNumberOfEnum<MeetCategory>();
            MeetingList meetings = _filterData.FilterByCategory(index, meetingList);
            _meetingShowData.ShowAllItems(meetings);
            return;
        }
        public void MeetingsByType(MeetingList meetingList)
        {
            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter type number please"));
            foreach (int i in Enum.GetValues(typeof(MeetType)))
            {
                _dataVizualization.DisplayData("", Enum.GetName(typeof(MeetType), i), i, "Black", "White", writeLine: _dataVizualization.TableLine);
            }
            _dataVizualization.AskForEntry("Number");
            int index = _dataCheck.GetNumberOfEnum<MeetType>();
            MeetingList meetings = _filterData.FilterByType(index, meetingList);
            _meetingShowData.ShowAllItems(meetings);
            return;
        }
        public void MeetingsByDate(MeetingList meetingList)
        {
            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter date please"));
            _dataVizualization.AskForEntry("Date");
            MeetingList meetings = _filterData.FilterByDate(_dataCheck.GetDate(), meetingList);
            _meetingShowData.ShowAllItems(meetings);
            return;
        }
        public void MeetingsByNumberOfAttendees(MeetingList meetingList)
        {
            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter interval please"));
            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("(e.g. (num1,0) - up to num1 including num1"));
            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("(e.g. (num1,num2) - between num1 and num2 including num2"));
            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("(e.g. (0,num2) - over num2"));
            MeetingList meetings = _filterData.FilterByNumberOfAttendees(_dataCheck.GetInterval(), meetingList);
            _meetingShowData.ShowAllItems(meetings);
            return;
        }

    }
}
