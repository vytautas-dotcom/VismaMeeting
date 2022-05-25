namespace VismaMeeting_v2.Services.Messages
{
    public class UIMessages
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
        public void DisplayData(string title, object name = null, object id = null, object itemList = null, string userName = "", int numOfLines = 0,
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
                if (numOfLines == 0 && writeLine != null)
                {
                    writeLine?.Invoke(name, id);
                }

                else
                    for (int i = 0; i < numOfLines; i++)
                    {
                        if (i + 1 == numOfLines)
                            writeLine?.Invoke(name, id);
                        else
                        {
                            writeLine?.Invoke(name, id);
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
            ShowLine(title.Length, '-');
            ShowLineWithUserName(title.Length, name, ' ', ' ');
            ShowLine(title.Length, '-');
            Console.WriteLine(title);
            ShowLine(title.Length, '_');
        }
        private void ShowEnum<T>()
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                DisplayData("", backgroundColor: "Black", textColor: "Blue", showMessage: () =>
                    Console.WriteLine("{0,-15} - {1}", item, (int)item));
            }
        }
        public void InputInformationMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            ShowLine(message.Length + 1, '.');
            Console.Write($"{message}: ");
            Console.ResetColor();
        }
        public void WarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            ShowLine(message.Length, '.');
            Console.WriteLine($"{message}");
            ShowLine(message.Length, '\'');
            Console.ResetColor();
        }
        public void ActionInformation(string message, string userName = "")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            int leftOffSet = (Console.WindowWidth / 3);
            int topOffSet = (Console.WindowHeight / 2);
            if (string.IsNullOrEmpty(userName))
            {
                Console.SetCursorPosition(leftOffSet, topOffSet);
                ShowLine(message.Length + 10, '/');
                Console.SetCursorPosition(leftOffSet, topOffSet + 1);
                Console.Write("\\\\\\  ");
                Console.Write(message);
                Console.WriteLine("  ///");
                Console.SetCursorPosition(leftOffSet, topOffSet + 2);
                ShowLine(message.Length + 10, '\\');
            }
            else
            {
                Console.SetCursorPosition(leftOffSet, topOffSet);
                ShowLine(message.Length + 11 + userName.Length, '/');
                Console.SetCursorPosition(leftOffSet, topOffSet + 1);
                Console.Write("\\\\\\  ");
                Console.Write(message + " " + userName);
                Console.WriteLine("  ///");
                Console.SetCursorPosition(leftOffSet, topOffSet + 2);
                ShowLine(message.Length + 11 + userName.Length, '\\');
            }
            Console.ResetColor();
            Thread.Sleep(3500);
            Console.Clear();
        }
    }
}
