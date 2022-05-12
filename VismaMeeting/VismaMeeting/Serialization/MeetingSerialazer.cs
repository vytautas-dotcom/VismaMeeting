using Newtonsoft.Json;
using VismaMeeting.MeetingData;

namespace VismaMeeting.Serialization
{
    internal class MeetingSerialazer : ISerialazer<MeetingList>
    {
        public MeetingList Deserialize()
        {
            MeetingList meetingList = new MeetingList();

            using (StreamReader streamReader = new StreamReader("../../../DataFiles/meetings.json"))
            {
                string json = streamReader.ReadToEnd();
                meetingList = JsonConvert.DeserializeObject<MeetingList>(json);
            }
            return meetingList;
        }

        public void JsonSerialize(MeetingList meetingList)
        {
            using (StreamWriter jsonStream = File.CreateText("../../../DataFiles/meetings.json"))
            {
                var jss = new JsonSerializer();
                jss.Serialize(jsonStream, meetingList);
            }
        }
    }
}
