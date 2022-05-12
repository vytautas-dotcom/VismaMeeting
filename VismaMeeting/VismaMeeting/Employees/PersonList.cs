namespace VismaMeeting.Employees
{
    internal class PersonList : List<Person>
    {
        public void Add(Guid id, string name, Dictionary<Guid, DateTime> meetingDate)
            => Add(id, name, meetingDate);
    }
}
