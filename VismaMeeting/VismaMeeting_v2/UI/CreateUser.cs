using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Services.DataForMessages;

namespace VismaMeeting_v2.UI
{
    public class CreateUser
    {
        public delegate void CreateUserHandler(string message, string userName);
        public event CreateUserHandler Notify;
        private readonly PersonShowData _personShowData;
        private readonly DataInput _dataInput;
        private readonly MessagesData _messagesData;
        public CreateUser(PersonShowData personShowData, DataInput dataInput)
        {
            _personShowData = personShowData;
            _dataInput = dataInput;
            _messagesData = new MessagesData();
        }
        public User SelectUser(Persons persons)
        {
            User user = new User();
            _personShowData.ShowNamesIndexes(persons);
            int userNumber = _dataInput.Select(persons);
            user.Person = persons[userNumber];
            Notify?.Invoke(_messagesData.InformationMessages["UserChanged"], user.Person.Name);
            return user;
        }
    }
}
