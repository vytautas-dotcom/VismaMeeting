using VismaMeeting.Functions.Interfaces;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class BackToStart : ICommand
    {
        public override void Execute()
        {
            _controlPanel = new ControlPanel(consoleClear: true);
            _controlPanel.RunProgram();
        }
    }
}
