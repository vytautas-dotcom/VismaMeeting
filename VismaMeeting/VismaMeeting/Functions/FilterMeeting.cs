using VismaMeeting.Employees;
using VismaMeeting.Functions.Interfaces;
using VismaMeeting.MeetingData;
using VismaMeeting.Serialization;

namespace VismaMeeting.Functions
{
    internal class FilterMeeting : ICommand
    {
        public override void Execute()
        {
            _filterShowData.ShowFilterParameters();
            int index = _dataCheck.Select(_filterData.FilterParameterIndexes);
            var function = _filterShowData.SelectFilter(index);
            function?.Invoke(_meetingList);
        }
    }
}
