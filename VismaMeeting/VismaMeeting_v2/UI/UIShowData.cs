using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Commands;

namespace VismaMeeting_v2.UI
{
    public class UIShowData
    {
        private readonly MessagesData _messagesData;
        private readonly UIMessages _uIMessages;
        private readonly DataInput _dataInput;
        private List<Action> executeFunctions;
        public UIShowData(UIMessages uIMessages, DataInput dataInput)
        {
            _uIMessages = uIMessages;
            _dataInput = dataInput;
            _messagesData = new MessagesData(); ;
            executeFunctions = new List<Action>();
        }
        public int ShowFunctions()
        {
            string title = "Select function to execute";
            _uIMessages.DisplayData(title, "", 0,backgroundColor: "DarkGray",textColor: "DarkYellow", userName: SessionData.User.Person.Name, writeTitle: _uIMessages.TableTitle);
            foreach (KeyValuePair<string, int> entry in _messagesData.Functions)
            {
                _uIMessages.DisplayData("", entry.Key, entry.Value, backgroundColor: "Gray", textColor: "DarkMagenta",
                    writeLine: _uIMessages.TableLine);
            }
            _uIMessages.ShowLine(title.Length, '-');
            int index = _dataInput.Select(_messagesData.FunctionsIndexes);
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
                default: 
                    break;
            }
        }
    }
}
