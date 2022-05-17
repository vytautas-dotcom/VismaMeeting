using VismaMeeting_v2.Models;

namespace VismaMeeting_v2.Services.DataDisplay
{
    public class PersonShowData
    {
        private readonly DataVisualization _dataVisualization;
        public PersonShowData(DataVisualization dataVisualization)
        {
            _dataVisualization = dataVisualization;
        }
        public void ShowOneItem(Person person)
        {
            int count = 0;
            _dataVisualization.ShowLine(50);
            Console.WriteLine("{0,-20} - {1}", "Name", person.Name);
            if (person.PersonMeetings.Count > 0)
                foreach (KeyValuePair<Guid, DateTime> entry in person.PersonMeetings)
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value);
                }
            _dataVisualization.ShowLine(50);
        }

        public void ShowAllItems(Persons personList)
            => personList.ForEach(x => ShowOneItem(x));

        public void ShowNamesIndexes(Persons personList, bool consoleClear = false)
        {
            string title = "Enter number which user you are";
            _dataVisualization.DisplayData(title, "", 0, "DarkGray", "Gray", consoleClear, writeTitle: _dataVisualization.WrapedTitle);
            personList.ForEach(x => _dataVisualization
                .DisplayData("", x.Name, personList.IndexOf(x), "Gray", "DarkBlue", writeLine: _dataVisualization.TableLine));
            _dataVisualization.ShowLine(title.Length);
        }

        public void ShowNamesIndexesNotAddedYet(Persons personList, List<Person> meetingPersons)
        {
            string title = "Select person's number to add to the meeting";
            _dataVisualization.DisplayData(title, "", 0, "DarkGray", "Gray", writeTitle: _dataVisualization.WrapedTitle);
            foreach (var person in personList)
            {
                if (!IsInMeetingPersonsList(person.Id, meetingPersons))
                    _dataVisualization.DisplayData("", person.Name, personList.IndexOf(person), "Gray", "DarkBlue", writeLine: _dataVisualization.TableLine);

            }
            _dataVisualization.ShowLine(title.Length);
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
