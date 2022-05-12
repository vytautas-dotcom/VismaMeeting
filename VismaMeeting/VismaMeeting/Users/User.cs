using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;

namespace VismaMeeting.Users
{
    internal abstract class User
    {
        protected Person Person;
        protected bool IsResponsible = false;
        public abstract void SetResponsibility(bool isResponsible);
        List<ICommand> Commands;
        public abstract void SetCommands(List<ICommand> commands);
        public abstract void PerformCommands(List<ICommand> commands);

    }
}
