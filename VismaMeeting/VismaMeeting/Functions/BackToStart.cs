using VismaMeeting.Functions.Interfaces;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class BackToStart : ICommand
    {
        public void Create()
        {
            new ControlPanel();
        }
    }
}
