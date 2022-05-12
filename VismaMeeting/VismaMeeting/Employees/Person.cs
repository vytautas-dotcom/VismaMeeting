namespace VismaMeeting.Employees
{
    internal class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //Dictionary contains Meeting's id and date when person was added
        public Dictionary<Guid, DateTime> PersonMeetings { get; set; }
    }
}
