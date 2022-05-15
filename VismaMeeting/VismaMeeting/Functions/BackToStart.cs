using VismaMeeting.Functions.Interfaces;
using VismaMeeting.UI;

namespace VismaMeeting.Functions
{
    internal class BackToStart : ICommand
    {
        ControlPanel controlPanel;
        public override void Execute()
        {
            controlPanel = new ControlPanel(consoleClear: true);
            controlPanel.RunProgram();
        }
    }
}
