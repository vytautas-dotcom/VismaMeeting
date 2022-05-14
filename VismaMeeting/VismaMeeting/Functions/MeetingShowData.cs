using System.Reflection;
using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;

namespace VismaMeeting.Functions
{
    internal class MeetingShowData : IShowData<Meeting, MeetingList>
    {
        DataVisualization _dataVisualization;
        public MeetingShowData()
        {
            _dataVisualization = new DataVisualization();
        }
        public void ShowOneItem(Meeting meeting)
        {
            int count = 0;
            string title = $"Selected meeting: {meeting.Name.ToUpper()}";
            _dataVisualization.DisplayData(title, "", 0, "DarkGray", "Gray", writeTitle: _dataVisualization.WrapedTitle);

            foreach (PropertyInfo prop in meeting.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(List<Person>))
                    continue;
                _dataVisualization
                .DisplayData("", prop.Name, prop.GetValue(meeting, null), "Gray", "DarkBlue", writeLine: _dataVisualization.TableLine);
            }
            _dataVisualization
                .DisplayData("", "", 0, "Gray", "Black", showMessage: () => Console.WriteLine("Meeting participants:" ));
            meeting.Persons?.ForEach(x => _dataVisualization
                .DisplayData("", x.Name, count++, "Gray", "DarkBlue", writeLine: _dataVisualization.TableLine));
            _dataVisualization.ShowLine(title.Length);
        }

        public void ShowAllItems(MeetingList meetingList)
            => meetingList.ForEach(x => ShowOneItem(x));

        public void ShowNamesIndexes(MeetingList meetingList)
        {
            string title = "Enter number of the meeting";
            _dataVisualization.DisplayData(title, "", 0, "DarkGray", "Gray", writeTitle: _dataVisualization.WrapedTitle);
            meetingList.ForEach(x => _dataVisualization
                .DisplayData("", x.Name, meetingList.IndexOf(x), "Gray", "DarkBlue", writeLine: _dataVisualization.TableLine));
            _dataVisualization.ShowLine(title.Length);
        }

        private void ShowOneItem(string title, object item)
            => Console.WriteLine("{0,-20} - {1}", title, item);

        private void ShowLine(int numberOfChar)
            => Console.WriteLine(new String('-', numberOfChar));
    }
}
