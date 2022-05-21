namespace VismaMeeting_v2.Services.DataForMessages
{
    public class MessagesData
    {
        public Dictionary<string, string> WarningMessages;
        public Dictionary<string, string> InformationMessages;
        public Dictionary<string, string> MeetingCreateMessages;
        public MessagesData()
        {
            WarningMessages = new Dictionary<string, string>();
            WarningMessages.Add("InputWarning", "You have to make correct input");
            WarningMessages.Add("FunctionWarning", "No function under such a choice");
            WarningMessages.Add("DateWarning", "End date can not be less than start date");
            WarningMessages.Add("NoMeetingsToAdd", "There is not any meeting to add person");
            WarningMessages.Add("NoMeetingsToDelete", "There is not any meeting to delete");
            WarningMessages.Add("FullMeetings", "All meetings are full added");
            WarningMessages.Add("ConfirmWarning", "Are you sure you want to continue (y/n)");
            WarningMessages.Add("MeetingDeleteWarning", "Only responsible person can delete the meeting");
            WarningMessages.Add("DeleteResponsiblePersonWarning", "Responsible person can not be deleted");
            WarningMessages.Add("AddedPersonWarning", "This person is already added");
            WarningMessages.Add("ImpossibleIntervalWarning", "Interval (0, 0) is impossible ;D");

            InformationMessages = new Dictionary<string, string>();
            InformationMessages.Add("CreationSuccess", "You have successufully created new meeting");
            InformationMessages.Add("DeleteSuccess", "You have successufully deleted meeting");
            InformationMessages.Add("AddPersonSuccess", "You have successufully added person to meeting");
            InformationMessages.Add("RemovePersonSuccess", "You have successufully removed person from meeting");

            MeetingCreateMessages = new Dictionary<string, string>();
            MeetingCreateMessages.Add("Name", "Enter meeting's name");
            MeetingCreateMessages.Add("Description", "Enter meeting's description");
            MeetingCreateMessages.Add("Category", "Choose meeting's number of category");
            MeetingCreateMessages.Add("Type", "Choose meeting's type");
            MeetingCreateMessages.Add("StartDate", "Enter meeting's start date");
            MeetingCreateMessages.Add("EndDate", "Enter meeting's end date");


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
