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

        public CreateUser(PersonSerialazer personSerialazer, DataCheck dataCheck, bool consoleClear)
        {
            _personSerialazer = personSerialazer;
            _personShowData = new PersonShowData();
            _dataVisualization = new DataVisualization();
            _personList = new PersonList();
            _personList = _personSerialazer.Deserialize();
            _personShowData.ShowNamesIndexes(_personList, consoleClear);
            _dataCheck = dataCheck;
        }
        public Person SelectUser()
        {
            Person person = new Person();
            int userNumber = _dataCheck.Select(_personList);
            person = _personList[userNumber];
            return person;
        }
    }
}
