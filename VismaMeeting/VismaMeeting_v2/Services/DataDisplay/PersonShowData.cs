using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Models;

namespace VismaMeeting_v2.Services.DataDisplay
{
    public class PersonShowData
    {
        private readonly UIMessages _uIMessages;
        public PersonShowData(UIMessages uIMessages)
        {
            _uIMessages = uIMessages;
        }
        public void ShowOneItem(Person person)
        {
            int count = 0;
            _uIMessages.ShowLine(50, '-');
            Console.WriteLine("{0,-20} - {1}", "Name", person.Name);
            if (person.PersonMeetings.Count > 0)
                foreach (KeyValuePair<Guid, DateTime> entry in person.PersonMeetings)
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value);
                }
            _uIMessages.ShowLine(50, '-');
        }

        public void ShowAllItems(Persons personList)
            => personList.ForEach(x => ShowOneItem(x));

        public void ShowNamesIndexes(Persons personList, bool consoleClear = false)
        {
            string title = "Enter number which user you are";
            _uIMessages.DisplayData(title, "", 0, backgroundColor: "DarkGray", textColor: "Gray", writeTitle: _uIMessages.TableTitle);
            personList.ForEach(x => _uIMessages
                .DisplayData("", x.Name, personList.IndexOf(x), backgroundColor: "Gray", textColor: "DarkBlue",
                writeLine: _uIMessages.TableLine));
            _uIMessages.ShowLine(title.Length, '-');
        }

        public void ShowNamesIndexesNotAddedYet(Persons personList, List<Person> meetingPersons)
        {
            string title = "Select person's number to add to the meeting";
            _uIMessages.DisplayData(title, "", 0, backgroundColor: "DarkGray", textColor: "Gray", writeTitle: _uIMessages.TableTitle);
            foreach (var person in personList)
            {
                if (!IsInMeetingPersonsList(person.Id, meetingPersons))
                    _uIMessages.DisplayData("", person.Name, personList.IndexOf(person),
                        backgroundColor: "Gray", textColor: "DarkBlue", writeLine: _uIMessages.TableLine);

            }
            _uIMessages.ShowLine(title.Length, '-');
        }
        private bool IsInMeetingPersonsList(Guid id, List<Person> meetingPersons)
        {
            bool isInList = false;
            foreach (var item in meetingPersons)
            {
                if (item.Id == id)
                    isInList = true;
            }
            return isInList;
        }
    }
}
