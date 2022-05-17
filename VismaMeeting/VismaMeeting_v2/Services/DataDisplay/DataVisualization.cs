namespace VismaMeeting_v2.Services.DataDisplay
{
    public class DataVisualization
    {
        public void ShowLine(int numberOfChar)
            => Console.WriteLine(new String('-', numberOfChar));

        public void DisplayData(string title, object name, object id, string backgroundColor, string textColor, bool clearConsole = false,
                            Action<string> writeTitle = null, Action<object, object> writeLine = null, 
                            Action showMessage = null)
        {
            if (clearConsole)
            {
                Console.Clear();
            }
            if (string.IsNullOrEmpty(backgroundColor) || string.IsNullOrEmpty(textColor))
            {
                WrapedTitle(title);
                return;
            }

            if (!ConsoleColor.TryParse(backgroundColor, true, out ConsoleColor colorBack) ||
                !ConsoleColor.TryParse(textColor, true, out ConsoleColor colorText))
            {
                WrapedTitle(title);
                return;
            }
            else
            {
                Console.BackgroundColor = colorBack;
                Console.ForegroundColor = colorText;
                writeTitle?.Invoke(title);
                writeLine?.Invoke(name, id);
                showMessage?.Invoke();
                Console.ResetColor();
            }

        }
        public void TableLine(object name, object id)
        {
            Console.WriteLine("{0,-20} - {1}", name, id);
        }
        public void WrapedTitle(string title)
        {
            ShowLine(title.Length);
            Console.WriteLine(title);
            ShowLine(title.Length);
        }
        public void AskForEntry(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{text}: ");
            Console.ResetColor();
        }
    }
}
