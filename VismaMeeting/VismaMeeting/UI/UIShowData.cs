using VismaMeeting.Functions;
using VismaMeeting.Users;

namespace VismaMeeting.UI
{
    internal class UIShowData
    {
        public delegate void ExecuteFunction();
        private readonly DataVisualization _dataVisualization;
        private readonly UIData _uIData;
        private readonly User _user;
        ExecuteFunction functionExecuter;
        public UIShowData(User user)
        {
            _dataVisualization = new DataVisualization();
            _uIData = new UIData();
            _user = user;
        }
        public void ShowFunctions()
        {
            string title = "Select function to execute";
            _dataVisualization.DisplayData(title, "", 0, "DarkGray", "DarkYellow", writeTitle: _dataVisualization.WrapedTitle);
            foreach (KeyValuePair<string, int> entry in _uIData.Functions)
            {
                _dataVisualization.DisplayData("", entry.Key, entry.Value, "Gray", "DarkMagenta", writeLine: _dataVisualization.TableLine);
            }
            _dataVisualization.ShowLine(title.Length);
        }
        public ExecuteFunction SelectFunction(int index)
        {
            switch (index)
            {
                case 0:
                    return
                    functionExecuter = _user.UserFunctions.CreateMeeting.Execute;
                case 1:
                    return
                    functionExecuter = _user.UserFunctions.DeleteMeeting.Execute;
                case 2:
                    return
                    functionExecuter = _user.UserFunctions.AddPerson.Execute;
                case 3:
                    return
                    functionExecuter = _user.UserFunctions.RemovePerson.Execute;
                case 4:
                    return
                    functionExecuter = _user.UserFunctions.FilterMeeting.Execute;
                case 5:
                    return
                    functionExecuter = _user.UserFunctions.BackToStart.Execute;
                default: return null;
            }
        }
    }
}
