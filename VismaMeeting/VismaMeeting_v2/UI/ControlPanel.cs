using Microsoft.Extensions.DependencyInjection;
using VismaMeeting_v2.Commands;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;

namespace VismaMeeting_v2.UI
{
    internal class ControlPanel
    {
        public IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            return serviceCollection
                .AddSingleton<UIShowData>()
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
        public void Run()
        {
            var serviceProvider = ConfigureServices();
            IServiceScope serviceScope = serviceProvider.CreateScope();

            List<Action> actions = new List<Action>();
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<MeetingsManagement>().Create());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<MeetingsManagement>().DeleteMeeting());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<PersonsManagement>().AddPerson());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<PersonsManagement>().RemovePerson());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<FilterManagement>().Filter());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<FilterManagement>().CreateUser());

            serviceScope.ServiceProvider.GetRequiredService<UIShowData>().SetFunctionsToList(actions);
            int index = serviceScope.ServiceProvider.GetRequiredService<UIShowData>().ShowFunctions();

            serviceScope.ServiceProvider.GetRequiredService<UIShowData>().SelectFunction(index);
        }
    }
}
