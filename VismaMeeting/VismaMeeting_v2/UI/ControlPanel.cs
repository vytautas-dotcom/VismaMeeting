using Microsoft.Extensions.DependencyInjection;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Commands;

namespace VismaMeeting_v2.UI
{
    internal class ControlPanel
    {
        public IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            return serviceCollection
                
                .AddSingleton<DataVisualization>()
                .AddSingleton<DataCheck>()
                .AddSingleton<IDbService<Persons>, DbService<Persons>>()
                .AddSingleton<IDbService<Meetings>, DbService<Meetings>>()
                .AddSingleton<CreateUser>()
                .AddSingleton<FilterData>()
                .AddSingleton<PersonMeetingData>()
                .AddSingleton<PersonShowData>()
                .AddSingleton<MeetingShowData>()
                .AddSingleton<FilterShowData>()
                .AddSingleton<MeetingsManagement>()
                .AddSingleton<PersonsManagement>()
                .AddSingleton<FilterManagement>()
                .BuildServiceProvider();
        }
        
    }
}
