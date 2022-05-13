using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaMeeting.Employees;
using VismaMeeting.MeetingData;

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
        public string GetIndex()
            => Console.ReadLine();
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
        public int? SelectMeetig(MeetingList meetingList, Person person)
        {
            bool isInputNumber;
            int? index = null;
            do
            {
                Console.WriteLine("Please select number of meeting or just press any other button to exit");
                isInputNumber = Int32.TryParse(GetIndex(), out int selectedIndex);
                if (!isInputNumber)
                    break;
                else if (CheckIndex(selectedIndex, meetingList))
                {
                    Console.WriteLine("Wrong index, please try again");
                    return SelectMeetig(meetingList, person);
                }
                else if (!IsPersonResponsible(person, meetingList[selectedIndex]))
                {
                    Console.WriteLine("Only the responsible person can delete the meeting");
                    return SelectMeetig(meetingList, person);
                }
                index = selectedIndex;
            } while (!isInputNumber && index == null);
            return index;
        }

        public bool CheckIndex(int index, MeetingList meetingList)
            => index >= meetingList.Count || index < 0;
        public bool IsPersonResponsible(Person person, Meeting meeting)
            => meeting.ResponsiblePersonId == person.Id;
    }
}
