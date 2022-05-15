using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;

namespace VismaMeeting.Functions
{
    internal class PersonShowData : IShowData<Person, PersonList>
    {
        DataVisualization _visualization;
        public PersonShowData()
        {
            _visualization = new DataVisualization();
        }
        public void ShowOneItem(Person person)
        {
            int count = 0;
            _visualization.ShowLine(50);
            Console.WriteLine("{0,-20} - {1}", "Name", person.Name);
            if(person.PersonMeetings.Count > 0)
                foreach (KeyValuePair<Guid, DateTime> entry in person.PersonMeetings)
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value);
                }
            _visualization.ShowLine(50);
        }

        public void ShowAllItems(PersonList personList)
            => personList.ForEach(x => ShowOneItem(x));

        public void ShowNamesIndexes(PersonList personList, bool consoleClear = false)
        {
            string title = "Enter number which user you are";
            _visualization.DisplayData(title, "", 0, "DarkGray", "Gray", consoleClear, writeTitle: _visualization.WrapedTitle);
            personList.ForEach(x => _visualization
                .DisplayData("", x.Name, personList.IndexOf(x), "Gray", "DarkBlue", writeLine: _visualization.TableLine));
            _visualization.ShowLine(title.Length);
        }

        public void ShowNamesIndexesNotAddedYet(PersonList personList, List<Person> meetingPersons)
        {
            string title = "Select person's number to add to the meeting";
            _visualization.DisplayData(title, "", 0, "DarkGray", "Gray", writeTitle: _visualization.WrapedTitle);
            foreach (var person in personList)
            {
                if(!IsInMeetingPersonsList(person.Id, meetingPersons))
                    _visualization.DisplayData("", person.Name, personList.IndexOf(person), "Gray", "DarkBlue", writeLine: _visualization.TableLine);
                   
            }
            _visualization.ShowLine(title.Length);
        }
        private bool IsInMeetingPersonsList(Guid id, List<Person> meetingPersons)
        {
            bool isInList = false;
            foreach (var item in meetingPersons)
            {
                if(item.Id == id)
                    isInList = true;
            }
            return isInList;
        }
    }
}
