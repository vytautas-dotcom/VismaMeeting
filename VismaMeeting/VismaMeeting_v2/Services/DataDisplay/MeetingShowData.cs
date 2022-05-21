using System.Reflection;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.Messages;

namespace VismaMeeting_v2.Services.DataDisplay
{
    public class MeetingShowData
    {
        private readonly DataVisualization _dataVisualization;
        private readonly UIMessages _uIMessages;
        private readonly MessagesData _messagesData;
        public MeetingShowData(DataVisualization dataVisualization, UIMessages uIMessages)
        {
            _dataVisualization = dataVisualization;
            _uIMessages = uIMessages;
            _messagesData = new MessagesData();
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
                .DisplayData("", "", 0, "Gray", "Black", showMessage: () => Console.WriteLine("Meeting participants:"));
            meeting.Persons?.ForEach(x => _dataVisualization
                .DisplayData("", x.Name, count++, "Gray", "DarkBlue", writeLine: _dataVisualization.TableLine));
            _dataVisualization.ShowLine(title.Length);
        }

        public void ShowAllItems(Meetings meetingList)
            => meetingList.ForEach(x => ShowOneItem(x));

        public void ShowNamesIndexes(Meetings meetings, bool consoleClear = false)
        {
            string title = "Enter number of the meeting";
            _uIMessages.DisplayData(title, "", 0, backgroundColor: "DarkGray",textColor: "Gray", writeTitle: _uIMessages.TableTitle);
            meetings.ForEach(x => _uIMessages
                .DisplayData("", x.Name, meetings.IndexOf(x), backgroundColor: "Gray",textColor: "DarkBlue", writeLine: _uIMessages.TableLine));
            //_dataVisualization.ShowLine(title.Length);
        }

        private void ShowOneItem(string title, object item)
            => Console.WriteLine("{0,-20} - {1}", title, item);

        private void ShowLine(int numberOfChar)
            => Console.WriteLine(new String('-', numberOfChar));
    }
}
