using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.UI;

namespace VismaMeeting_v2.Services.DataOperations
{
    public class DataCheck
    {
        private readonly DataVisualization _dataVisualization;
        public DataCheck(DataVisualization dataVisualization)
        {
            _dataVisualization = dataVisualization;
        }
        #region Meeting creation
        public Meeting CreateMeeting(Person person, PersonMeetingData personMeetingData)
        {
            Meeting meeting = new Meeting();
            meeting.Id = Guid.NewGuid();
            //personMeetingData.AddResponsiblePersonToMeeting(meeting, person);
            meeting.ResponsiblePerson = person.Name;
            meeting.Persons = new List<Person>();
            meeting.Persons.Add(person);
            _dataVisualization.AskForEntry("Enter meeting's name");
            meeting.Name = GetData();
            _dataVisualization.AskForEntry("Enter meeting's description");
            meeting.Description = GetData();
            ShowEnum<MeetCategory>();
            _dataVisualization.AskForEntry("Choose meeting's number of category");
            meeting.Category = (MeetCategory)GetNumberOfEnum<MeetCategory>();
            ShowEnum<MeetType>();
            _dataVisualization.AskForEntry("Choose meeting's type");
            meeting.Type = (MeetType)GetNumberOfEnum<MeetType>();
            _dataVisualization.AskForEntry("Enter meeting's start date");
            meeting.StartDate = GetDate();
            _dataVisualization.AskForEntry("Enter meeting's end date");
            DateTime endDate = GetDate();
            if (endDate < meeting.StartDate)
            {
                while (endDate < meeting.StartDate)
                {
                    _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("End date can not be less than start date!"));
                    endDate = GetDate();
                }
            }
            meeting.EndDate = endDate;
            return meeting;
        }
        private void ShowEnum<T>()
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine("{0,-15} - {1}", item, (int)item);
            }
        }
        #endregion
        public int Select<T>(List<T> list)
        {
            _dataVisualization.AskForEntry("Number");
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
        public bool CheckIndex<T>(int index, List<T> list)
            => index >= list.Count || index < 0;
        public string GetData()
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("Please enter required data"));
                GetData();
            }
            return input;
        }
        #region Filter meetings by attendees number
        public (int, int) GetInterval()
        {
            (int, int) interval;
            _dataVisualization.AskForEntry("First number");
            if (!int.TryParse(GetData(), out interval.Item1))
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("Please enter required data"));
                GetInterval();
            }
            _dataVisualization.AskForEntry("Second number");
            if (!int.TryParse(GetData(), out interval.Item2))
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("Please enter required data"));
                GetInterval();
            }
            if (interval.Item1 == 0 && interval.Item2 == 0)
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("Interval (0, 0) is impossible ;D"));
                GetInterval();
            }
            return interval;
        }
        #endregion
        //datacheking
        public DateTime GetDate()
        {
            Console.Write("(e.g. mm/dd/yyyy or mm/dd): ");
            bool isDateCorrect = DateTime.TryParse(GetData(), out DateTime date);
            if (!isDateCorrect)
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("Please enter required data"));
                GetDate();
            }
            return date;
        }
        public int GetNumberOfEnum<T>()
        {
            bool isNumberOfCategory = Int32.TryParse(GetData(), out int numberOfCategory);
            if (!isNumberOfCategory || !Enum.IsDefined(typeof(T), numberOfCategory))
            {
                _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () => Console.WriteLine("Please enter required data"));
                return GetNumberOfEnum<T>();
            }
            return numberOfCategory;
        }
        #region Delete meeting
        public int SelectMeetigForDelete(Meetings meetingList, Person person)
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
                    _dataVisualization.DisplayData("", "", 0, "Black", "Green", showMessage: () =>
                    Console.WriteLine("Do you want to continue? (y/any key)"));
                    bool yes = char.TryParse(Console.ReadLine(), out char result);
                    if ('y' != result || !yes)
                    {
                        ControlPanel controlPanel = new ControlPanel();
                        controlPanel.Run();
                    }
                    else
                        SelectMeetigForDelete(meetingList, person);
                }
                //
                //if (!_dataCheck.Confirm())
                //{
                //    Console.Clear();
                //    ControlPanel controlPanel = new ControlPanel();
                //    controlPanel.Run();
                //}

                success = true;
            } while (!success);
            return index;
        }
        //datachecking
        public bool IsMeetingToDeleteForPerson(Meetings meetingList, Person person)
        {
            bool isToDelete = false;
            foreach (var meeting in meetingList)
            {
                if (IsPersonResponsible(person, meeting))
                    isToDelete = true;
            }
            return isToDelete;
        }
        #endregion
        #region Remove person
        public int SelectPersonToRemoveFromMeeting(Meeting meeting)
        {
            bool success = false;
            int index;
            do
            {
                index = Select(meeting.Persons);
                if (IsPersonResponsible(meeting.Persons[index], meeting))
                {
                    _dataVisualization.DisplayData("", "", 0, "Black", "Red", showMessage: () =>
                    Console.WriteLine("Responsible person can not be deleted"));
                    return SelectPersonToRemoveFromMeeting(meeting);
                }
                success = true;
            } while (!success);
            return index;
        }
        #endregion
        public int SelectPersonForMeeting(Meeting meeting, Persons personList)
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
        public bool IsPersonAdded(Person person, Meeting meeting)
        {
            bool isAdded = false;
            foreach (var item in meeting.Persons)
            {
                if (item.Id == person.Id)
                    isAdded = true;
            }
            return isAdded;
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
                //datachecking
                if (!char.TryParse(Console.ReadLine(), out char confirmationLetter))
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
        //datachecking
        public bool IsPersonResponsible(Person person, Meeting meeting)
            => meeting.ResponsiblePerson == person.Name;



    }
}
