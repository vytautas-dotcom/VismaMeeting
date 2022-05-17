using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;

namespace VismaMeeting_v2.UI
{
    public class UIShowData
    {
        private readonly DataVisualization _dataVisualization;
        private readonly UIData _uIData;
        private readonly DataCheck _dataCheck;
        private List<Action> _commands;
        public UIShowData(DataVisualization dataVisualization, DataCheck dataCheck)
        {
            _dataVisualization = dataVisualization;
            _dataCheck = dataCheck;
            _uIData = new UIData();
            _commands = new List<Action>();
        }
        public int ShowFunctions()
        {
            string title = "Select function to execute";
            _dataVisualization.DisplayData(title, "", 0, "DarkGray", "DarkYellow", writeTitle: _dataVisualization.WrapedTitle);
            foreach (KeyValuePair<string, int> entry in _uIData.Functions)
            {
                _dataVisualization.DisplayData("", entry.Key, entry.Value, "Gray", "DarkMagenta", writeLine: _dataVisualization.TableLine);
            }
            _dataVisualization.ShowLine(title.Length);
            int index = _dataCheck.Select(_uIData.FunctionsIndexes);
            return index;
        }
        public void SetFunctionsToList(List<Action> commands)
        {
            _commands = commands;
        }
        public void SelectFunction(int index)
        {
            switch (index)
            {
                case 1:
                    _commands[index].Invoke();
                    break;
                case 2:
                    _commands[index].Invoke();
                    break;
                case 3:
                    _commands[index].Invoke();
                    break;
                case 4:
                    _commands[index].Invoke();
                    break;
                case 5:
                    _commands[index].Invoke();
                    break;
                case 6:
                    _commands[index].Invoke();
                    break;
                default: break;
                        
            }
        }
    }
}
