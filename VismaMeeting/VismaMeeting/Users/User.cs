using VismaMeeting.Employees;

namespace VismaMeeting.Users
{
    internal class  User
    {
        public Person Person { get; set; }
        public UserFunctions UserFunctions;
        public User(Person person)
        {
            Person = person;
        }
        public void SetUserFunctions(UserFunctions userFunctions)
        {
            UserFunctions = userFunctions;
        }
    }
}
