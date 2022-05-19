namespace VismaMeeting_v2.Services.DataForMessages
{
    public class MeesagesData
    {
        Dictionary<string, string> warningMessages;
        Dictionary<string, string> informationMessages;
        public MeesagesData()
        {
            warningMessages = new Dictionary<string, string>();
            warningMessages.Add("InputWarning", "You have to make correct input");
            warningMessages.Add("FunctionWarning", "No function under such a choice");
            warningMessages.Add("DateWarning", "End date can not be less than start date");
            warningMessages.Add("ConfirmWarning", "Are you sure you want to continue (y/n)");
            warningMessages.Add("MeetingDeleteWarning", "Only the responsible person can delete the meeting");
            warningMessages.Add("DeleteResponsiblePersonWarning", "Responsible person can not be deleted");
            warningMessages.Add("AddedPersonWarning", "This person is already added");
            warningMessages.Add("ImpossibleIntervalWarning", "Interval (0, 0) is impossible ;D");

            informationMessages = new Dictionary<string, string>();
            informationMessages.Add("CreationSuccess", "You have successufully created new meeting");
            informationMessages.Add("DeleteSuccess", "You have successufully deleted meeting");
            informationMessages.Add("AddPersonSuccess", "You have successufully added person to meeting");
            informationMessages.Add("RemovePersonSuccess", "You have successufully removed person from meeting");
        }
        /// <summary>
        /// List of functions for main menu
        /// </summary>
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
        public List<int> FunctionsIndexes = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6 });

        /// <summary>
        /// List of functions for filter menu
        /// </summary>
        public Dictionary<string, int> FilterParameters = new Dictionary<string, int>()
        {
            {"Description", 0},
            {"Responsible person", 1},
            {"Category", 2},
            {"Type", 3},
            {"Date", 4},
            {"Attendees number", 5}
        };
        public List<int> FilterParameterIndexes = new List<int>(new[] { 0, 1, 2, 3, 4, 5 });
    }
}
