using VismaMeeting.Employees;

namespace VismaMeeting.Users
{
    internal class  User
    {
        public Person Person { get; set; }
        public bool IsResponsible {get; set;}
        public UserFunctions UserFunctions;
        public User(Person person)
        {
            Person = person;
        }

        public void SetResponsibility(bool isResponsible)
        {
            IsResponsible = isResponsible;
        }
        public void SetUserFunctions(UserFunctions userFunctions)
        {
            UserFunctions = userFunctions;
        }
        public void ExecuteFunctions()
        {
            if (IsResponsible)
            {

            }
        }
    }
}
