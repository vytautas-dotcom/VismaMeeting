using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Models;
using System.Reflection;
using VismaMeeting_v2.Commands;

namespace VismaMeeting_v2.Services.DataDisplay
{
    public class MeetingShowData
    {
        private readonly UIMessages _uIMessages;
        private readonly MessagesData _messagesData;
        public MeetingShowData(UIMessages uIMessages)
        {
            _uIMessages = uIMessages;
            _messagesData = new MessagesData();
        }
        public void ShowOneItem(Meeting meeting)
        {
            int count = 0;
            string title = $"Selected meeting: {meeting.Name.ToUpper()}";
            _uIMessages.DisplayData(title, "", 0, backgroundColor: "DarkGray", textColor: "Gray", writeTitle: _uIMessages.TableTitle);
            foreach (PropertyInfo prop in meeting.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(List<Person>))
                    continue;
                _uIMessages.DisplayData("", prop.Name, prop.GetValue(meeting, null), 
                                        backgroundColor: "Gray", textColor: "DarkBlue", writeLine: _uIMessages.TableLine);
            }
            _uIMessages
                .DisplayData("", "", 0, backgroundColor: "Gray", textColor: "Black", 
                             showMessage: () => Console.WriteLine("Meeting participants:"));
            meeting.Persons?.ForEach(x => _uIMessages
                                            .DisplayData("", x.Name, count++, backgroundColor: "Gray", textColor: "DarkBlue", 
                                                         writeLine: _uIMessages.TableLine));
            _uIMessages.ShowLine(title.Length, '-');
        }

        public void ShowAllItems(Meetings meetingList)
            => meetingList.ForEach(x => ShowOneItem(x));

        public void ShowNamesIndexes(Meetings meetings, bool consoleClear = false)
        {
            string title = "Enter number of the meeting";
            _uIMessages.DisplayData(title, "", 0, backgroundColor: "DarkGray",textColor: "Gray", userName: SessionData.User.Person.Name,
                                    writeTitle: _uIMessages.TableTitle);
            meetings.ForEach(x => _uIMessages
                .DisplayData("", x.Name, meetings.IndexOf(x), backgroundColor: "Gray",textColor: "DarkBlue", writeLine: _uIMessages.TableLine));
        }

        private void ShowOneItem(string title, object item)
            => Console.WriteLine("{0,-20} - {1}", title, item);
    }
}
