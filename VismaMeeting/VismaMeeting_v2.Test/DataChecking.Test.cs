using System;
using VismaMeeting_v2.Services.Checking;
using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.Messages;
using Xunit;

namespace VismaMeeting_v2.Test
{
    public class DataCheckingTest
    {
        private readonly DataChecking _dataChecking;
        private readonly MessagesData _messageData;
        public DataCheckingTest()
        {
            _dataChecking = new DataChecking(new UIMessages());
            _messageData = new MessagesData();
        }
        
        [Fact]
        public void ShouldReturnTrueAndGiveOutNumber()
        {
            string expected = "5";
            bool result1 = _dataChecking.IsInputNumber(expected,out int result2);

            Assert.Equal(true, result1);
            Assert.Equal(5, result2);
        }
        [Fact]
        public void ShouldReturnTrueAndOutDate()
        {

            bool result1 = _dataChecking.IsInputDate("05/05/2022", out DateTime result2);

            Assert.Equal(true, result1);
            Assert.Equal(DateTime.Parse("05/05/2022"), result2);
        }
    }
}