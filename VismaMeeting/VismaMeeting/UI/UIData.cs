namespace VismaMeeting.UI
{
    internal class UIData
    {
        public Dictionary<string, int> Functions = new Dictionary<string, int>()
        {
            {"Create Meeting", 0},
            {"Delete Meeting", 1},
            {"Add a Person", 2},
            {"Remove a Person", 3},
            {"Filter Meetings", 4},
            {"Change User", 5},
            {"Exit", 6}
        };
        public List<int> FunctionsIndexes = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6});
    }
}
