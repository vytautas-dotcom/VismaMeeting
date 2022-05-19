using Xunit;
using VismaMeeting_v2.Services.Input;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.Messages;
using System;

namespace VismaMeeting_v2.Test
{
    public class DataInputTest
    {
    private readonly DataChecking _dataChecking = new DataChecking();
    private readonly IWrite _write = new UIMessages();
        
        [Fact]
        public void ShouldReturn10WhenGivenString10()
        {

            DataInput dataInput = new DataInput(_dataChecking, _write);

            int expected = 10;
            int output;
            dataInput.InputNumber("Before", "After", out output, expected.ToString());

            Assert.Equal(expected, output);
        }
        [Fact]
        public void ShouldReturnCorrectDateWhenGivenStringDate()
        {

            DataInput dataInput = new DataInput(_dataChecking, _write);

            DateTime expected = DateTime.Parse("05/19/2022");
            DateTime output;
            dataInput.InputDate("Before", "After", out output, expected.ToString());

            Assert.Equal(expected, output);
        }
    }
}