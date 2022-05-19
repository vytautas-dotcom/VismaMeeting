namespace VismaMeeting_v2.Services.Messages
{
    public class UIMessages : IWrite
    {
        public void ShowLine(int numOfSymbols, char symbol)
            => Console.WriteLine(new String(symbol, numOfSymbols));

        public void ShowLineWithUserName(int titleLength, string name, char symbolBefor, char SymbolAfter)
        {
            int halfOfTitleLength = (titleLength - name.Length) / 2;
            Console.Write(new String(symbolBefor, halfOfTitleLength));
            Console.Write(name);
            Console.WriteLine(new String(SymbolAfter, halfOfTitleLength));
        }
        public void ShowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            ShowLine(message.Length, '=');
            Console.Write($"{message}: ");
            ShowLine(message.Length, '=');
            Console.ResetColor();
        }
        public void DisplayData(string title, object name = null, object id = null, object itemList = null, string userName = "user", int numOfLines = 0,
                                string backgroundColor = "DarkGray", string textColor = "Gray", bool clearConsole = false,
                                Action<string, string> writeTitle = null, Action<object, object> writeLine = null, Action showMessage = null)
        {
            if (clearConsole)
            {
                Console.Clear();
            }
            if (!ConsoleColor.TryParse(backgroundColor, true, out ConsoleColor colorBack) ||
                !ConsoleColor.TryParse(textColor, true, out ConsoleColor colorText))
            {
                throw new ArgumentException("Incorrect color name");
            }
            else
            {
                Console.BackgroundColor = colorBack;
                Console.ForegroundColor = colorText;
                writeTitle?.Invoke(title, userName);
                showMessage?.Invoke();
                if (numOfLines == 0)
                    writeLine?.Invoke(name, id);
                else
                    for (int i = 0; i < numOfLines; i++)
                    {
                        if (i + 1 == numOfLines)
                            writeLine?.Invoke(name, id);
                        else
                        {
                            writeLine?.Invoke(name, id);
                            ShowLine(22 + name.ToString().Length, '.');
                        }
                    }
                Console.ResetColor();
            }

        }
        public void TableLine(object name, object id)
        {
            Console.WriteLine("{0,-20} - {1}", name, id);
        }
        public void TableTitle(string title, string name)
        {
            ShowLine(title.Length, '*');
            ShowLineWithUserName(title.Length, name, '(', ')');
            ShowLine(title.Length, '*');
            Console.WriteLine(title);
            ShowLine(title.Length, '*');
        }
        private void ShowEnum<T>()
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                DisplayData("", backgroundColor: "Black", textColor: "Blue", showMessage: () =>
                    Console.WriteLine("{0,-15} - {1}", item, (int)item));
            }
        }
    }
}
