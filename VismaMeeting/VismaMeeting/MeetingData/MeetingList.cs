using VismaMeeting.Employees;

namespace VismaMeeting.MeetingData
{
    internal class MeetingList : List<Meeting>
    {
        public void Add(Guid id, string name, Person responsiblePerson, List<Person> participants,
                    string description, MeetCategory category, MeetType type, DateTime startDate,
                    DateTime endDate)
            => Add(id, name, responsiblePerson, participants, description, category, type, startDate, endDate);

    }
}
