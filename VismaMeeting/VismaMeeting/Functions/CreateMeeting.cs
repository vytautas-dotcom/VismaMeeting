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
        private readonly PersonSerialazer _personSerialazer;
        private readonly DataCheck _dataCheck;
        private readonly PersonMeetingData _personMeetingData;

        public CreateMeeting(Person person, MeetingList meetingList, MeetingSerialazer meetingSerialazer,
                             PersonSerialazer personSerialazer, DataCheck dataCheck, PersonMeetingData personMeetingData)
        {
            _person = person;
            _meetingSerialazer = meetingSerialazer;
            _personSerialazer = personSerialazer;
            _meetingList = _meetingSerialazer.Deserialize() ?? meetingList;
            _dataCheck = dataCheck;
            _personMeetingData = personMeetingData;

        }
        public void Create()
        {
            Meeting meeting = GetUserInput();
            _meetingList.Add(meeting);
            _meetingSerialazer.JsonSerialize(_meetingList);
            PersonList personList = _personSerialazer.Deserialize();
            personList.RemoveAt(personList.FindIndex(x => x.Id == _person.Id));
            personList.Add(_person);
            _personSerialazer.JsonSerialize(personList);
        }

        private Meeting GetUserInput()
        {
            Meeting meeting = new Meeting();
            meeting.Id = Guid.NewGuid();
            _personMeetingData.AddResponsiblePersonToMeeting(meeting, _person);
            meeting.ResponsiblePersonId = _person.Id;
            meeting.Persons = new List<Person>();
            meeting.Persons.Add(_person);
            Console.WriteLine("Enter meeting's name");
            meeting.Name = _dataCheck.GetData();
            Console.WriteLine("Enter meeting's description");
            meeting.Description = _dataCheck.GetData();
            ShowEnum<MeetCategory>();
            Console.WriteLine("Choose meeting's number of category");
            meeting.Category = (MeetCategory)_dataCheck.GetNumberOfEnum<MeetCategory>();
            Console.WriteLine(meeting.Category);
            ShowEnum<MeetType>();
            Console.WriteLine("Choose meeting's type");
            meeting.Type = (MeetType)_dataCheck.GetNumberOfEnum<MeetType>();
            Console.WriteLine("Enter meeting's start date");
            meeting.StartDate = _dataCheck.GetDate();
            Console.WriteLine("Enter meeting's end date");
            DateTime endDate = _dataCheck.GetDate();
            if (endDate < meeting.StartDate)
            {
                while (endDate < meeting.StartDate)
                {
                    Console.WriteLine("End date can not be less than start date!");
                    endDate = _dataCheck.GetDate();
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
    }
}
