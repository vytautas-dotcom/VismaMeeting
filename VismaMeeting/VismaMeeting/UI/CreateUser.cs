using VismaMeeting.Employees;
using VismaMeeting.Functions;
using VismaMeeting.Serialization;

namespace VismaMeeting.UI
{
    internal class CreateUser
    {
        private readonly PersonList _personList;
        private readonly PersonSerialazer _personSerialazer;
        private readonly PersonShowData _personShowData;

        public CreateUser(PersonList personList, PersonSerialazer personSerialazer)
        {
            _personList = personList;
            _personSerialazer = personSerialazer;
            _personShowData = new PersonShowData();
            _personList = _personSerialazer.Deserialize();
            _personShowData.ShowNamesIndexes(_personList);
        }
        public Person SelectUser()
        {
            Person person = new Person();
            Console.WriteLine("Enter number which user you are");
            int userNumber = Convert.ToInt32(Console.ReadLine());
            person = _personList[userNumber];
            Console.WriteLine(new String('-', 50));
            Console.WriteLine($"Hello {person.Name}, select function please");
            return person;
        }
    }
}
