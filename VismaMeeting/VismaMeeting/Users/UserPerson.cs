using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;

namespace VismaMeeting.Users
{
    internal class UserPerson : User
    {
        public UserPerson(Person person)
        {
            this.Person = person;
        }
        public override void PerformCommands(List<ICommand> commands)
        {
            throw new NotImplementedException();
        }

        public override void SetCommands(List<ICommand> commands)
        {
            throw new NotImplementedException();
        }

        public override void SetResponsibility(bool isResponsible)
        {
            this.IsResponsible = isResponsible;
        }
    }
}
