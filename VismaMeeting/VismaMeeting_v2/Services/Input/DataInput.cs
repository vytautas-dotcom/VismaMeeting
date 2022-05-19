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
            if (!string.IsNullOrEmpty(stringInput))
            {
                _write.ShowMessage(messageAfterInput);
                string input = stringInput;
                bool success = _dataChecking.IsInputNumber(input, out output);
                if (!_dataChecking.IsInputNotNullOrEmptySpace(input) && !success)
                {
                    _write.ShowMessage(messageAfterInput);
                    InputNumber(messageBeforeInput, messageAfterInput, out output);
                }
            }
            else
            {
                _write.ShowMessage(messageAfterInput);
                string input = Input();
                bool success = _dataChecking.IsInputNumber(input, out output);
                if (!_dataChecking.IsInputNotNullOrEmptySpace(input) && !success)
                {
                    _write.ShowMessage(messageAfterInput);
                    InputNumber(messageBeforeInput, messageAfterInput, out output);
                }
            }
        }
        public void InputDate(string messageBeforeInput, string messageAfterInput, out DateTime output, string stringInput = null)
        {
            if (!string.IsNullOrEmpty(stringInput))
            {
                _write.ShowMessage(messageAfterInput);
                string input = stringInput;
                bool success = _dataChecking.IsInputDate(input, out output);
                if (!_dataChecking.IsInputNotNullOrEmptySpace(input) && !success)
                {
                    _write.ShowMessage(messageAfterInput);
                    InputDate(messageBeforeInput, messageAfterInput, out output);
                }

            }
            else
            {
                _write.ShowMessage(messageAfterInput);
                string input = Input();
                bool success = _dataChecking.IsInputDate(input, out output);
                if (!_dataChecking.IsInputNotNullOrEmptySpace(input) && !success)
                {
                    _write.ShowMessage(messageAfterInput);
                    InputDate(messageBeforeInput, messageAfterInput, out output);
                }
            }
        }
    }
}
