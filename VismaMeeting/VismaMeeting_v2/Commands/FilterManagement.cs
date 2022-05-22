using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.UI;

namespace VismaMeeting_v2.Commands
{
    internal class FilterManagement : Management
    {
        private readonly FilterShowData _filterShowData;
        public FilterManagement(IDbService<Meetings> dbServiceM, IDbService<Persons> dbServiceP, MeetingShowData meetingShowData,
                                PersonShowData personShowData, PersonMeetingData personMeetingData, CreateUser createUser,
                                FilterShowData filterShowData, UIMessages uIMessages, DataChecking dataChecking, 
                                DataInput dataInput) :
            base(dbServiceP, dbServiceM, meetingShowData, personShowData, personMeetingData, createUser,
                 uIMessages, dataChecking, dataInput)
        {
            _filterShowData = filterShowData;
        }

        public void Filter()
        {
            _filterShowData.ShowFilterParameters();
            int index = _filterShowData.AskForIndex();
            var function = _filterShowData.SelectFilter(index);
            function?.Invoke(_meetings, _persons);
        }
    }
}
