namespace VismaMeeting_v2.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Dictionary<Guid, DateTime> PersonMeetings { get; set; }
    }
    public class Persons : List<Person>
    {
    }    
}
