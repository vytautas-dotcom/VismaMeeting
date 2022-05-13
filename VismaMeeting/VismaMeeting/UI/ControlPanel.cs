using VismaMeeting.Employees;
using VismaMeeting.Functions;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;
using VismaMeeting.Users;

namespace VismaMeeting.UI
{
    internal class ControlPanel
    {
        private readonly CreateUser _createUser;
        private readonly MeetingSerialazer _meetingSerialazer;
        private readonly PersonSerialazer _personSerialazer;
        private readonly DataCheck _dataCheck;
        public MeetingList MeetingList { get; set; }
        public PersonList PersonList { get; set; }
        public User User { get; set; }
        public UserFunctions UserFunctions { get; set; }
        public ControlPanel()
        {
            _meetingSerialazer = new MeetingSerialazer();
            _personSerialazer = new PersonSerialazer();
            MeetingList = new MeetingList();
            PersonList = new PersonList();
            _dataCheck = new DataCheck();
            _createUser = new CreateUser(PersonList, _personSerialazer);
            User = new User(_createUser.SelectUser());
            UserFunctions = new UserFunctions();
            UserFunctions.CreateMeeting = new CreateMeeting(User.Person, MeetingList, _meetingSerialazer, _dataCheck);
        }
        public void SetFunctions()
        {
            User.SetUserFunctions(UserFunctions);
        }
        
    }
}
