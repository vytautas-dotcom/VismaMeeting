
using VismaMeeting.Functions.Interfaces;

namespace VismaMeeting.Users
{
    internal class UserFunctions
    {
        public ICommand CreateMeeting;
        public ICommand DeleteMeeting;
        public ICommand AddPerson;
        public ICommand RemovePerson;
        public ICommand FilterMeeting;
    }
}
