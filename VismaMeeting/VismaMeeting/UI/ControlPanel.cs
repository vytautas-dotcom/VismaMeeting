using VismaMeeting.Employees;
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
        public MeetingList MeetingList { get; set; }
        public PersonList PersonList { get; set; }
        public User User { get; set; }
        public ControlPanel()
        {
            _meetingSerialazer = new MeetingSerialazer();
            _personSerialazer = new PersonSerialazer();
            MeetingList = _meetingSerialazer.Deserialize();
            PersonList = _personSerialazer.Deserialize();
            _createUser = new CreateUser(PersonList, _personSerialazer);
            User = new UserPerson(_createUser.SelectUser());
        }
    }
}
