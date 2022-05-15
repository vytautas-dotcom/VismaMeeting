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
        private readonly DataVisualization _dataVisualization;
        private readonly DataCheck _dataCheck;

        public CreateUser(PersonList personList, PersonSerialazer personSerialazer, DataCheck dataCheck)
        {
            _personSerialazer = personSerialazer;
            _personShowData = new PersonShowData();
            _dataVisualization = new DataVisualization();
            _personList = _personSerialazer.Deserialize();
            _personShowData.ShowNamesIndexes(_personList);
            _dataCheck = dataCheck;
        }
        public Person SelectUser()
        {
            Person person = new Person();
            int userNumber = _dataCheck.Select(_personList);
            person = _personList[userNumber];
            Console.WriteLine(new String('-', 50));
            _dataVisualization.DisplayData("", "", 0, "Gray", "DarkGreen", showMessage: () =>
            Console.WriteLine($"Hello {person.Name}, select function please"));
            return person;
        }
    }
}
