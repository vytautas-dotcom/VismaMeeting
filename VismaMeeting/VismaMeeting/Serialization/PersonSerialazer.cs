using Newtonsoft.Json;
using VismaMeeting.Employees;

namespace VismaMeeting.Serialization
{
    internal class PersonSerialazer : ISerialazer<PersonList>
    {
        public PersonList Deserialize()
        {
            PersonList personList = new PersonList();

            using (StreamReader streamReader = new StreamReader("../../../DataFiles/persons.json"))
            {
                string json = streamReader.ReadToEnd();
                personList = JsonConvert.DeserializeObject<PersonList>(json);
            }
            return personList;
        }

        public void JsonSerialize(PersonList personList)
        {
            using (StreamWriter jsonStream = File.CreateText("../../../DataFiles/persons.json"))
            {
                var jss = new JsonSerializer();
                jss.Serialize(jsonStream, personList);
            }
        }
    }
}
