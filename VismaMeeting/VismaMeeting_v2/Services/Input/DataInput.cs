using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.Messages;

namespace VismaMeeting_v2.Services.Input
{
    public class DataInput
    {
        private readonly DataChecking _dataChecking;
        private readonly UIMessages _uIMessages;
        private readonly MessagesData _messagesData;
        public DataInput(DataChecking dataChecking, UIMessages uIMessages)
        {
            _dataChecking = dataChecking;
            _uIMessages = uIMessages;
            _messagesData = new MessagesData();
        }
        private string Input()
            => Console.ReadLine();
        public string InputString(string messageBeforeInput, string messageAfterInput)
        {
            _uIMessages.InputInformationMessage(messageBeforeInput);
            string input = Input();
            if (_dataChecking.IsInputNotNullOrEmptySpace(input))
                return input;
            _uIMessages.WarningMessage(messageAfterInput);
            InputString(messageBeforeInput, messageAfterInput);
            return null;
        }

        public void InputNumber(string messageBeforeInput, string messageAfterInput, out int output, string stringInput = null)
        {
            string input;
            if (!string.IsNullOrEmpty(stringInput))
                input = stringInput;
            else
                input = InputString(messageBeforeInput, messageAfterInput);
            bool success = _dataChecking.IsInputNumber(input, out output);
            if (!_dataChecking.IsInputNotNullOrEmptySpace(input) && !success)
            {
                _uIMessages.WarningMessage(messageAfterInput);
                InputNumber(messageBeforeInput, messageAfterInput, out output);
            }
        }
        public void InputDate(string messageBeforeInput, string messageAfterInput, out DateTime output, string stringInput = null)
        {
            string input;
            if (!string.IsNullOrEmpty(stringInput))
                input = stringInput;
            else
                input = InputString(messageBeforeInput, messageAfterInput);
            bool success = _dataChecking.IsInputDate(input, out output);
            if (!success)
            {
                _uIMessages.WarningMessage(messageAfterInput);
                InputDate(messageBeforeInput, messageAfterInput, out output);
            }
        }
        public void EnumInput<T>(string messageBeforeInput, string messageAfterInput, out int output, string stringInput = null)
        {
            InputNumber(messageBeforeInput, messageAfterInput, out output, stringInput);
            if (!Enum.IsDefined(typeof(T), output))
            {
                _uIMessages.WarningMessage(messageAfterInput);
                EnumInput<T>(messageBeforeInput, messageAfterInput, out output, stringInput);
            }
        }

        public bool Continue()
        {
            _uIMessages.InputInformationMessage(_messagesData.WarningMessages["ConfirmWarning"]);
            return _dataChecking.IsConfimationSuccessful(InputString("Confirm",
                _messagesData.WarningMessages["InputWarning"]));
        }
    }
}
