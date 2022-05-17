using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace VismaMeeting_v2.Models
{
    public enum MeetCategory
    {
        [EnumMember(Value = "CodeMonkey")]
        CodeMonkey = 1,
        [EnumMember(Value = "Hub")]
        Hub = 2,
        [EnumMember(Value = "Short")]
        Short = 3,
        [EnumMember(Value = "TeamBuilding")]
        TeamBuilding = 4
    }
    public enum MeetType
    {
        [EnumMember(Value = "Live")]
        Live = 1,
        [EnumMember(Value = "InPerson")]
        InPerson = 2
    }
    public class Meeting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public List<Person> Persons { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MeetCategory Category { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MeetType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class Meetings : List<Meeting>
    {
    }
}
