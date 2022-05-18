namespace VismaMeeting_v2.Services.Messages
{
    public interface IWrite
    {
        void ShowMessage(string message);
        void ShowLine(int numOfSymbols, char symbol);
    }
}
