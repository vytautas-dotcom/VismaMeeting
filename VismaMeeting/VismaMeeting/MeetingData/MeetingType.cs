using System.Runtime.Serialization;

namespace VismaMeeting.MeetingData
{
    enum MeetType
    {
        [EnumMember(Value = "Live")]
        Live = 1,
        [EnumMember(Value = "InPerson")]
        InPerson = 2
    }
}
