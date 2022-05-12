using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;

namespace VismaMeeting.Functions
{
    internal class MeetingShowData : IShowData<Meeting, MeetingList>
    {
        public void ShowOneItem(Meeting meeting)
        {
            int count = 0;
            Console.WriteLine(new String('-', 50));
            Console.WriteLine("{0,-20} - {1}", "Name", meeting.Name);
            Console.WriteLine("{0,-20} - {1}", "Responsible Person", meeting.ResponsiblePersonId);
            meeting.Persons?.ForEach(x => Console.WriteLine("{0,-20} - {1} - {2}", "Person", x.Name, count++));
            Console.WriteLine("{0,-20} - {1}", "Description", meeting.Description);
            Console.WriteLine("{0,-20} - {1}", "Category", meeting.Category);
            Console.WriteLine("{0,-20} - {1}", "Type", meeting.Type);
            Console.WriteLine("{0,-20} - {1}", "Start Date", meeting.StartDate);
            Console.WriteLine("{0,-20} - {1}", "End Date", meeting.EndDate);
            Console.WriteLine(new String('-', 50));
        }

        public void ShowAllItems(MeetingList meetingList)
            => meetingList.ForEach(x => ShowOneItem(x));

        public void ShowNamesIndexes(MeetingList meetingList)
            => meetingList.ForEach(x => Console.WriteLine("{0,-20} - {1}", x.Name, meetingList.IndexOf(x)));
    }
}
