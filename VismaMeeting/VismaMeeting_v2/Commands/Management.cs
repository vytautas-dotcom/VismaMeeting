using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;

namespace VismaMeeting_v2.Commands
{
    public abstract class Management
    {
        public User User { get; set; }
        internal readonly IDbService<Persons> _dbServiceP;
        internal readonly IDbService<Meetings> _dbServiceM;
        internal readonly DataCheck _dataCheck;
        internal readonly DataVisualization _dataVisualization;
        internal readonly MeetingShowData _meetingShowData;
        internal readonly PersonShowData _personShowData;
        internal readonly PersonMeetingData _personMeetingData;

        public Management(IDbService<Persons> dbServiceP, IDbService<Meetings> dbServiceM, DataCheck dataCheck,
                          DataVisualization dataVisualization, MeetingShowData meetingShowData, PersonShowData personShowData,
                          PersonMeetingData personMeetingData)
        {
            _dbServiceP = dbServiceP;
            _dbServiceM = dbServiceM;
            _dataCheck = dataCheck;
            _dataVisualization = dataVisualization;
            _meetingShowData = meetingShowData;
            _personShowData = personShowData;
            _personMeetingData = personMeetingData;
        }
    }
}
