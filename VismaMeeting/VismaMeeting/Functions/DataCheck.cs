using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeeting.Functions
{
    internal class DataCheck
    {
        public string Input { get; set; }
        public string GetData()
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter required data");
                GetData();
            }
            return Input = input;
        }
        public DateTime GetDate()
        {
            Console.Write("(e.g. 05/12/2022): ");
            bool isDateCorrect = DateTime.TryParse(GetData(), out DateTime date);
            if (!isDateCorrect)
            {
                Console.WriteLine("Please enter correct date");
                GetDate();
            }
            return date;
        }
        public int GetNumberOfEnum<T>()
        {
            bool isNumberOfCategory = Int32.TryParse(GetData(), out int numberOfCategory);
            if (!isNumberOfCategory || !Enum.IsDefined(typeof(T), numberOfCategory))
            {
                Console.WriteLine("Please enter correct number");
                return GetNumberOfEnum<T>();
            }
            return numberOfCategory;
        }
    }
}
