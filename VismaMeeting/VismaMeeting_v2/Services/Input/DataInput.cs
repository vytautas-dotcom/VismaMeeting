using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.Messages;

namespace VismaMeeting_v2.Services.Input
{
    public class DataInput
    {
        private readonly DataChecking _dataChecking;
        private readonly IWrite _write;
        public DataInput(DataChecking dataChecking, IWrite write)
        {
            _dataChecking = dataChecking;
            _write = write;
        }
        private string Input()
            => Console.ReadLine();
        public string InputString(string messageBeforeInput, string messageAfterInput)
        {
            _write.ShowMessage(messageAfterInput);
            string input = Input();
            if (_dataChecking.IsInputNotNullOrEmptySpace(input))
                return input;
            _write.ShowMessage(messageAfterInput);
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
            _write.ShowMessage(messageAfterInput);
            bool success = _dataChecking.IsInputNumber(input, out output);
            if (!_dataChecking.IsInputNotNullOrEmptySpace(input) && !success)
            {
                _write.ShowMessage(messageAfterInput);
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
            _write.ShowMessage(messageAfterInput);
            bool success = _dataChecking.IsInputDate(input, out output);
            if (!_dataChecking.IsInputNotNullOrEmptySpace(input) && !success)
            {
                _write.ShowMessage(messageAfterInput);
                InputDate(messageBeforeInput, messageAfterInput, out output);
            }
        }
        public void EnumInput<T>(string messageBeforeInput, string messageAfterInput, out int output, string stringInput = null)
        {
            InputNumber(messageBeforeInput, messageAfterInput, out output, stringInput);
            if (!Enum.IsDefined(typeof(T), output))
            {
                _write.ShowMessage(messageAfterInput);
                EnumInput<T>(messageBeforeInput, messageAfterInput, out output, stringInput);
            }
        }
    }
}
