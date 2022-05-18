using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;

namespace VismaMeeting_v2.UI
{
    public class UIShowData
    {
        private readonly DataVisualization _dataVisualization;
        private readonly UIData _uIData;
        private readonly DataCheck _dataCheck;
        private List<Action> executeFunctions;
        public UIShowData(DataVisualization dataVisualization, DataCheck dataCheck)
        {
            _dataVisualization = dataVisualization;
            _uIData = new UIData();
            _dataCheck = dataCheck;
            executeFunctions = new List<Action>();
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
        public void SetFunctionsToList(List<Action> functions)
        {
            executeFunctions = functions;
        }
        public void SelectFunction(int index)
        {
            switch (index)
            {
                case 0:
                    executeFunctions[index].Invoke();
                    break;
                case 1:
                    executeFunctions[index].Invoke();
                    break;
                case 2:
                    executeFunctions[index].Invoke();
                    break;
                case 3:
                    executeFunctions[index].Invoke();
                    break;
                case 4:
                    executeFunctions[index].Invoke();
                    break;
                case 5:
                    executeFunctions[index].Invoke();
                    break;
                default: break;

            }
        }
    }
}
