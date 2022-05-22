using Microsoft.Extensions.DependencyInjection;
using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Commands;
using VismaMeeting_v2.Models;

namespace VismaMeeting_v2.UI
{
    internal class ControlPanel
    {
        public IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            return serviceCollection
                .AddSingleton<IDbService<Meetings>, DbService<Meetings>>()
                .AddSingleton<IDbService<Persons>, DbService<Persons>>()
                .AddSingleton<UIMessages>()
                .AddSingleton<DataChecking>()
                .AddSingleton<DataInput>()
                .AddSingleton<UIShowData>()
                .AddSingleton<CreateUser>()
                .AddSingleton<FilterData>()
                .AddSingleton<PersonMeetingData>()
                .AddSingleton<PersonShowData>()
                .AddSingleton<MeetingShowData>()
                .AddSingleton<FilterShowData>()
                .AddSingleton<Management>()
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
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<MeetingsManagement>().CreateMeeting());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<MeetingsManagement>().DeleteMeeting());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<PersonsManagement>().AddPerson());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<PersonsManagement>().RemovePerson());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<FilterManagement>().Filter());
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<Management>().CreateUser(true));
            actions.Add(() => serviceScope.ServiceProvider.GetRequiredService<Management>().Exit());


            serviceScope.ServiceProvider.GetRequiredService<UIShowData>().SetFunctionsToList(actions);
            if (SessionData.User.Person == null)
                serviceScope.ServiceProvider.GetRequiredService<Management>().CreateUser();
            int index = serviceScope.ServiceProvider.GetRequiredService<UIShowData>().ShowFunctions();
            serviceScope.ServiceProvider.GetRequiredService<UIShowData>().SelectFunction(index);
        }
    }
}
