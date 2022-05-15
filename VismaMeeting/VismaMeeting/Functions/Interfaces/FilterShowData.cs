﻿using VismaMeeting.MeetingData;

namespace VismaMeeting.Functions.Interfaces
{
    internal class FilterShowData
    {
        public delegate void ExecuteFunction(MeetingList meetingList);
        private readonly FilterData _filterData;
        private readonly DataVisualization _dataVizualization;
        private readonly DataCheck _dataCheck;
        private readonly MeetingShowData _meetingShowData;
        ExecuteFunction functionExecuter;
        public FilterShowData()
        {
            _filterData = new FilterData();
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
            _dataVizualization.DisplayData("", "", 0, "White", "Black", showMessage: () => Console.WriteLine("Enter text please"));
            _dataVizualization.AskForEntry("Text");
            MeetingList meetings = _filterData.FilterByDescription(_dataCheck.GetData(), meetingList);
            _meetingShowData.ShowAllItems(meetings);
            return;
        }

        public void MeetingsByResponsiblePerson(MeetingList meetingList)
        {
            Console.WriteLine("By Responsible");
        }
        public void MeetingsByCategory(MeetingList meetingList)
        {
            Console.WriteLine("By Category");
        }
        public void MeetingsByType(MeetingList meetingList)
        {
            Console.WriteLine("By Type");
        }
        public void MeetingsByDate(MeetingList meetingList)
        {
            Console.WriteLine("By Date");
        }
        public void MeetingsByNumberOfAttendees(MeetingList meetingList)
        {
            Console.WriteLine("By NumberOfAttendee");
        }

    }
}