using VismaMeeting.Employees;
using VismaMeeting.MeetingData;

namespace VismaMeeting.Functions
{
    internal class DataCheck
    {
        DataVisualization _dataVisualization;
        public DataCheck()
        {
            _dataVisualization = new DataVisualization();
        }
        public int Select<T>(List<T> list)
        {
            int index;
            string input = Console.ReadLine();
            bool isCorrect = int.TryParse(input, out index);
            if (string.IsNullOrEmpty(input) || !isCorrect || CheckIndex(index, list))
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("Please enter required data"));
                return Select(list);
            }
            return index;
        }
        public string GetData()
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter required data");
                GetData();
            }
            return input;
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
        public int SelectMeetigForDelete(MeetingList meetingList, Person person)
        {
            bool success = false;
            int index;
            do
            {
                index = Select(meetingList);
                if (!IsPersonResponsible(person, meetingList[index]))
                {
                    _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () =>
                    Console.WriteLine("Only the responsible person can delete the meeting"));
                    return SelectMeetigForDelete(meetingList, person);
                }
                success = true;
            } while (!success);
            return index;
        }
        //public int? SelectMeetigForPerson(MeetingList meetingList, Person person)
        //{
        //    bool isInputNumber;
        //    int? index = null;
        //    do
        //    {
        //        _dataVisualization.AskForNumber();
        //        isInputNumber = Int32.TryParse(GetIndex(), out int selectedIndex);
        //        if (!isInputNumber)
        //            break;
        //        else if (CheckIndex(selectedIndex, meetingList))
        //        {
        //            Console.WriteLine("Wrong index, please try again");
        //            return SelectMeetigForPerson(meetingList, person);
        //        }
        //        index = selectedIndex;
        //    } while (!isInputNumber && index == null);
        //    return index;
        //}
        public int SelectPersonForMeeting(Meeting meeting, PersonList personList)
        {
            bool success = false;
            int index;
            do
            {
                index = Select(personList);
                if (IsPersonAdded(personList[index], meeting))
                {
                    _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () =>
                    Console.WriteLine("This person is already added"));
                    return SelectPersonForMeeting(meeting, personList);
                }
                success = true;
            } while (!success);
            return index;
        }
        public bool IsMeetingToDeleteForPerson(MeetingList meetingList, Person person)
        {
            bool isToDelete = false;
            foreach (var meeting in meetingList)
            {
                if (IsPersonResponsible(person, meeting))
                    isToDelete = true;
            }
            return isToDelete;
        }
        public bool Confirm()
        {
            _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => 
            Console.WriteLine("Are you sure you want to continue? (y/n)"));
            bool success = true;
            bool continueCycle = true;
            char confirmation = 'y';
            do
            {
                if (!char.TryParse(GetIndex(), out char confirmationLetter))
                {
                    _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("Please enter required data"));
                    return Confirm();
                }
                else if (confirmationLetter != confirmation)
                {
                    success = false;
                    continueCycle = false;
                }
                else
                {
                    success = true;
                    continueCycle = false;
                }
            } while (continueCycle);
            return success;
        }
        public bool CheckIndex<T>(int index, List<T> list)
            => index >= list.Count || index < 0;
        public bool IsPersonResponsible(Person person, Meeting meeting)
            => meeting.ResponsiblePersonId == person.Id;

        public bool IsPersonAdded(Person person, Meeting meeting)
        {
            bool isAdded = false;
            foreach (var item in meeting.Persons)
            {
                if(item.Id == person.Id)
                    isAdded = true;
            }
            return isAdded;
        }

    }
}
