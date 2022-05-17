using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;

namespace VismaMeeting_v2.UI
{
    public class CreateUser
    {
        private readonly PersonShowData _personShowData;
        private readonly DataVisualization _dataVisualization;
        private readonly DataCheck _dataCheck;

        public CreateUser(DataVisualization dataVisualization, DataCheck dataCheck, PersonShowData personShowData)
        {
            _personShowData = personShowData;
            _dataVisualization = dataVisualization;
            _dataCheck = dataCheck;
        }
        public User SelectUser(Persons persons)
        {
            User user = new User();
            _personShowData.ShowNamesIndexes(persons);
            int userNumber = _dataCheck.Select(persons);
            user.Person = persons[userNumber];
            return user;
        }
    }
}
