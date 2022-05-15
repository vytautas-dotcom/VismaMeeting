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
        private readonly PersonMeetingData _personMeetingData;
        private readonly IShowData<Meeting, MeetingList> _showMeetingData;
        private readonly IShowData<Person, PersonList> _showPersonData;
        private readonly DataVisualization _dataVisualization;
        public readonly UIShowData _uIShowData;
        public readonly UIData _uIData;
        public MeetingList MeetingList { get; set; }
        public PersonList PersonList { get; set; }
        public User User { get; set; }
        public UserFunctions UserFunctions { get; set; }
        public ControlPanel(bool consoleClear = false)
        {
            _meetingSerialazer = new MeetingSerialazer();
            _personSerialazer = new PersonSerialazer();
            MeetingList = new MeetingList();
            PersonList = new PersonList();
            _dataCheck = new DataCheck();
            _showMeetingData = new MeetingShowData();
            _showPersonData = new PersonShowData();
            _personMeetingData = new PersonMeetingData();
            _createUser = new CreateUser(PersonList, _personSerialazer, _dataCheck, consoleClear);
            User = new User(_createUser.SelectUser());
            UserFunctions = new UserFunctions();
            UserFunctions.CreateMeeting = new CreateMeeting(User.Person, MeetingList, _meetingSerialazer, _personSerialazer, _dataCheck, _personMeetingData);
            UserFunctions.DeleteMeeting = new DeleteMeeting(User.Person, MeetingList, _meetingSerialazer, _personSerialazer, _dataCheck, _showMeetingData, _personMeetingData);
            UserFunctions.AddPerson = new AddPerson(User.Person, MeetingList, PersonList, _meetingSerialazer, _personSerialazer, _dataCheck, _showMeetingData, _showPersonData, _personMeetingData);
            UserFunctions.RemovePerson = new RemovePerson(User.Person, MeetingList, PersonList, _meetingSerialazer, _personSerialazer, _dataCheck, _showMeetingData, _showPersonData, _personMeetingData);
            UserFunctions.FilterMeeting = new FilterMeeting(User.Person, MeetingList, PersonList, _meetingSerialazer, _personSerialazer, _dataCheck, _showMeetingData, _showPersonData, _personMeetingData);
            UserFunctions.BackToStart = new BackToStart();
            _uIShowData = new UIShowData(User);
            _uIData = new UIData();
        }
        public void SetFunctions()
        {
            User.SetUserFunctions(UserFunctions);
        }
        public void RunProgram()
        {
            SetFunctions();
            _uIShowData.ShowFunctions();
            int index = _dataCheck.Select(_uIData.FunctionsIndexes);
            var function = _uIShowData.SelectFunction(index);
            function?.Invoke();
        }
        
    }
}
