namespace VismaMeeting_v2.Services.Messages
{
    public class Warnings : IWrite
    {
        public void ShowLine(int numOfSymbols, char symbol)
            => Console.WriteLine(new String(symbol, numOfSymbols));

        public void ShowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            ShowLine(message.Length, '/');
            Console.Write($"{message}");
            ShowLine(message.Length, '\\');
            Console.ResetColor();
        }
    }
}
