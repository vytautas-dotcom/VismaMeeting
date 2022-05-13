using System.Runtime.Serialization;

namespace VismaMeeting.MeetingData
{
    enum MeetCategory
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
}
