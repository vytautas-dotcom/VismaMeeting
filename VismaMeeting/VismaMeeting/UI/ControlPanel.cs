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
        private readonly PersonSerialazer _personSerialazer;
        private readonly DataCheck _dataCheck;
        private readonly UIShowData _uIShowData;
        private readonly UIData _uIData;
        private readonly User _user;
        private readonly UserFunctions _userFunctions;
        public ControlPanel(bool consoleClear = false)
        {
            _personSerialazer = new PersonSerialazer();
            _dataCheck = new DataCheck();
            _createUser = new CreateUser(_personSerialazer, _dataCheck, consoleClear);
            _user = new User(_createUser.SelectUser());
            _userFunctions = new UserFunctions();
            _userFunctions.CreateMeeting = new CreateMeeting(_user.Person);
            _userFunctions.DeleteMeeting = new DeleteMeeting(_user.Person);
            _userFunctions.AddPerson = new AddPerson();
            _userFunctions.RemovePerson = new RemovePerson();
            _userFunctions.FilterMeeting = new FilterMeeting();
            _userFunctions.BackToStart = new BackToStart();
            _uIShowData = new UIShowData(_user);
            _uIData = new UIData();
        }
        public void SetFunctions()
        {
            _user.SetUserFunctions(_userFunctions);
        }
        public void RunProgram()
        {
            SetFunctions();
            _uIShowData.ShowFunctions();
            int index = _dataCheck.Select(_uIData.FunctionsIndexes);
            var function = _uIShowData.SelectFunction(index);
            function?.Invoke();
            return;
        }
        
    }
}
