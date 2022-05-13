using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VismaMeeting.Employees;

namespace VismaMeeting.MeetingData
{
    internal class Meeting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ResponsiblePersonId { get; set; }
        public List<Person> Persons { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MeetCategory Category { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MeetType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
