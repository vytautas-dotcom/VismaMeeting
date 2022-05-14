using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;

namespace VismaMeeting.Functions
{
    internal class PersonShowData : IShowData<Person, PersonList>
    {
        DataVisualization visualization;
        public PersonShowData()
        {
            visualization = new DataVisualization();
        }
        public void ShowOneItem(Person person)
        {
            int count = 0;
            visualization.ShowLine(50);
            Console.WriteLine("{0,-20} - {1}", "Name", person.Name);
            if(person.PersonMeetings.Count > 0)
                foreach (KeyValuePair<Guid, DateTime> entry in person.PersonMeetings)
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value);
                }
            visualization.ShowLine(50);
        }

        public void ShowAllItems(PersonList personList)
            => personList.ForEach(x => ShowOneItem(x));

        public void ShowNamesIndexes(PersonList personList)
        {
            string title = "Enter number which user you are";
            visualization.DisplayData(title, "", 0, "DarkGray", "Gray", writeTitle: visualization.WrapedTitle);
            personList.ForEach(x => visualization
                .DisplayData("", x.Name, personList.IndexOf(x), "Gray", "DarkBlue", writeLine: visualization.TableLine));
            visualization.ShowLine(title.Length);
        }

        public void ShowNamesIndexesNotAddedYet(PersonList personList, List<Person> meetingPersons)
        {
            string title = "Select person's number to add to the meeting";
            visualization.DisplayData(title, "", 0, "DarkGray", "Gray", writeTitle: visualization.WrapedTitle);
            foreach (var person in personList)
            {
                if(!IsInMeetingPersonsList(person.Id, meetingPersons))
                    visualization.DisplayData("", person.Name, personList.IndexOf(person), "Gray", "DarkBlue", writeLine: visualization.TableLine);
                   
            }
            visualization.ShowLine(title.Length);
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
