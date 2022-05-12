using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;

namespace VismaMeeting.Functions
{
    internal class PersonShowData : IShowData<Person, PersonList>
    {
        public void ShowOneItem(Person person)
        {
            int count = 0;
            Console.WriteLine(new String('-', 50));
            Console.WriteLine("{0,-20} - {1}", "Name", person.Name);
            if(person.PersonMeetings.Count > 0)
                foreach (KeyValuePair<Guid, DateTime> entry in person.PersonMeetings)
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value);
                }
            Console.WriteLine(new String('-', 50));
        }

        public void ShowAllItems(PersonList personList)
            => personList.ForEach(x => ShowOneItem(x));

        public void ShowNamesIndexes(PersonList personList)
            => personList.ForEach(x => Console.WriteLine("{0,-20} - {1}", x.Name, personList.IndexOf(x)));
    }
}
