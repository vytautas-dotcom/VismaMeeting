using Newtonsoft.Json;

namespace VismaMeeting_v2.Services.DataServices
{
    public class DbService<T> : IDbService<T> where T : new()
    {
        public T Get()
        {
            T list = new();
            string fileName = typeof(T).Name.ToLower();
            using (StreamReader streamReader = new StreamReader($"../../../Data/{fileName}.json"))
            {
                string json = streamReader.ReadToEnd();
                list = JsonConvert.DeserializeObject<T>(json);
            }
            return list;
        }

        public void Save(T list)
        {
            string fileName = typeof(T).Name.ToLower();
            using (StreamWriter jsonStream = File.CreateText($"../../../Data/{fileName}.json"))
            {
                var jss = new JsonSerializer();
                jss.Serialize(jsonStream, list);
            }
        }
    }
}
