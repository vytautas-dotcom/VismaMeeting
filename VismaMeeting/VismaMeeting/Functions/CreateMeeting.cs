using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;

namespace VismaMeeting.Functions
{
    internal class CreateMeeting : ICommand
    {
        private readonly Person _person;
        private readonly MeetingList _meetingList;
        private readonly MeetingSerialazer _meetingSerialazer;

        public CreateMeeting(Person person, MeetingList meetingList, MeetingSerialazer meetingSerialazer)
        {
            _person = person;
            _meetingList = meetingList;
            _meetingSerialazer = meetingSerialazer;
        }
        public override void Create()
        {
            Meeting meeting = GetUserInput();
            _meetingList.Add(meeting);
            _meetingSerialazer.JsonSerialize(_meetingList);
        }

        private Meeting GetUserInput()
        {
            Meeting meeting = new Meeting();
            meeting.Id = Guid.NewGuid();
            meeting.ResponsiblePersonId = _person.Id;
            meeting.Persons = new List<Person>();
            meeting.Persons.Add(_person);
            Console.WriteLine("Enter meeting's name");
            meeting.Name = GetData();
            Console.WriteLine("Enter meeting's description");
            meeting.Description = GetData();
            ShowEnum<MeetCategory>();
            Console.WriteLine("Choose meeting's number of category");
            meeting.Category = (MeetCategory)GetNumberOfEnum<MeetCategory>();
            Console.WriteLine(meeting.Category);
            ShowEnum<MeetType>();
            Console.WriteLine("Choose meeting's type");
            meeting.Type = (MeetType)GetNumberOfEnum<MeetType>();
            Console.WriteLine("Enter meeting's start date");
            meeting.StartDate = GetDate();
            Console.WriteLine("Enter meeting's end date");
            DateTime endDate = GetDate();
            if (endDate < meeting.StartDate)
            {
                while (endDate < meeting.StartDate)
                {
                    Console.WriteLine("End date can not be less than start date!");
                    endDate = GetDate();
                }
            }

            return meeting;
        }
        private string GetData()
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter required data");
                GetData();
            }
            return input;
        }
        private void ShowEnum<T>()
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine("{0,-15} - {1}", item, (int)item);
            }
        }
        private int GetNumberOfEnum<T>()
        {
            bool isNumberOfCategory = Int32.TryParse(GetData(), out int numberOfCategory);
            if (!isNumberOfCategory || !Enum.IsDefined(typeof(T), numberOfCategory))
            {
                Console.WriteLine("Please enter correct number");
                return GetNumberOfEnum<T>();
            }
            return numberOfCategory;
        }
        private DateTime GetDate()
        {
            Console.Write("(e.g. 05/12/2022): ");
            bool isDateCorrect = DateTime.TryParse(Console.ReadLine(), out DateTime date);
            if (!isDateCorrect)
            {
                Console.WriteLine("Please enter correct date");
                GetDate();
            }
            return date;
        }
    }
}
