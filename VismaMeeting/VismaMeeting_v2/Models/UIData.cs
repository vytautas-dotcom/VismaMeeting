namespace VismaMeeting_v2
{
    public class UIData
    {
        public Dictionary<string, int> Functions = new Dictionary<string, int>()
        {
            {"Create Meeting", 1},
            {"Delete Meeting", 2},
            {"Add a Person", 3},
            {"Remove a Person", 4},
            {"Filter Meetings", 5},
            {"Change User", 6},
            {"Exit", 7}
        };
        public List<int> FunctionsIndexes = new List<int>(new[] { 1, 2, 3, 4, 5, 6, 7});
    }
}
