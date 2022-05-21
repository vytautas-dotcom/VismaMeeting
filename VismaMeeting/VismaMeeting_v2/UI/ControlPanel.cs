using Microsoft.Extensions.DependencyInjection;
using VismaMeeting_v2.Commands;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.DataDisplay;
using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.DataOperations;
using VismaMeeting_v2.Services.DataServices;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Services.Messages;

namespace VismaMeeting_v2.UI
{
    internal class ControlPanel
    {
        public IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            return serviceCollection
                .AddSingleton<UIMessages>()
                .AddSingleton<DataChecking>()
                .AddSingleton<DataInput>()
                .AddSingleton<MessagesData>()
                //.AddSingleton<PersonMeetingData>()
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
            if (IManagement.User.Person == null)
                serviceScope.ServiceProvider.GetRequiredService<Management>().CreateUser();
            int index = serviceScope.ServiceProvider.GetRequiredService<UIShowData>().ShowFunctions();
            serviceScope.ServiceProvider.GetRequiredService<UIShowData>().SelectFunction(index);


            //Person person = new Person();
            //serviceScope.ServiceProvider.GetRequiredService<PersonMeetingData>().CreateMeeting(person);
        }
    }
}
