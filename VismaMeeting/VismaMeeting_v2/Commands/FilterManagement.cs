using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
using VismaMeeting_v2.UI;

namespace VismaMeeting_v2.Commands
{
    internal class FilterManagement : Management
    {
        public Meetings _meetings = new();
        public Persons _persons = new();
        private readonly CreateUser _createUser;
        private readonly FilterShowData _filterShowData;
        private readonly FilterData _filterData;
        public FilterManagement(IDbService<Meetings> dbServiceM, IDbService<Persons> dbServiceP,
                                  DataCheck dataCheck, DataVisualization dataVisualization, MeetingShowData meetingShowData,
                                  PersonShowData personShowData, PersonMeetingData personMeetingData, CreateUser createUser,
                                  FilterShowData filterShowData, FilterData filterData) :
            base(dbServiceP, dbServiceM, dataCheck, dataVisualization, meetingShowData, personShowData, personMeetingData)
        {
            _createUser = createUser;
            _filterShowData = filterShowData;
            _filterData = filterData;
        }

        public void GetAllItems()
        {
            _meetings = _dbServiceM.Get();
            _persons = _dbServiceP.Get();
        }
        public void CreateUser()
        {
            GetAllItems();
            User = _createUser.SelectUser(_persons);
        }
        public void Filter()
        {
            CreateUser();
            _filterShowData.ShowFilterParameters();
            int index = _dataCheck.Select(_filterData.FilterParameterIndexes);
            var function = _filterShowData.SelectFilter(index);
            function?.Invoke(_meetings, _persons);
        }
    }
}
