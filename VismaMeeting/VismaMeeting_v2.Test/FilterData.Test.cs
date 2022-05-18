using Xunit;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;

namespace VismaMeeting_v2.Test
{
    public class FilterDataTest
    {
        [Fact]
        public void ShouldReturnMeeting1andMeeting2WhenGivenInterval2_0()
        {
            var person1 = new Person();
            var person2 = new Person();
            var person3 = new Person();

            var meeting1 = new Meeting();
            meeting1.Persons.Add(person1);

            var meeting2 = new Meeting();
            meeting2.Persons.AddRange(new[] { person1, person2 });

            var meeting3 = new Meeting();
            meeting3.Persons.AddRange(new[] { person1, person2, person3 });

            var meetings1 = new Meetings();
            meetings1.AddRange(new[] { meeting1 });

            var meetings2 = new Meetings();
            meetings2.AddRange(new[] { meeting1, meeting2 });

            var meetings3 = new Meetings();
            meetings3.AddRange(new[] { meeting1, meeting2, meeting3 });

            var filter = new FilterData();

            var result = filter.FilterByNumberOfAttendees((2, 0), meetings3);
            var expected = new[] { meeting1, meeting2 };

            Assert.Equal(expected, result);
        }
    }
}