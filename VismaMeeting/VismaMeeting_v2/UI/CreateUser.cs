using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Models;

namespace VismaMeeting_v2.UI
{
    public class CreateUser
    {
        private readonly PersonShowData _personShowData;
        private readonly DataInput _dataInput;
        public CreateUser(PersonShowData personShowData, DataInput dataInput)
        {
            _personShowData = personShowData;
            _dataInput = dataInput;
        }
        public User SelectUser(Persons persons)
        {
            User user = new User();
            _personShowData.ShowNamesIndexes(persons);
            int userNumber = _dataInput.Select(persons);
            user.Person = persons[userNumber];
            return user;
        }
    }
}
