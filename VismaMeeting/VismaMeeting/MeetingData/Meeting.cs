using VismaMeeting.Employees;

namespace VismaMeeting.MeetingData
{
    internal class Meeting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Person ResponsiblePerson { get; set; }
        public List<Person> Persons { get; set; }
        public string Description { get; set; }
        public MeetCategory Category { get; set; }
        public MeetType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
