using Xunit;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using System.Collections.Generic;

namespace VismaMeeting_v2.Test
{
    public class FilterDataTest
    {
        
        [Fact]
        public void FilterByResponsiblePerson()
        {
            var person1 = new Person() { Name = "Tom" };
            var person2 = new Person() { Name = "John" };
            var person3 = new Person() { Name = "Tim" };

            var persons = new Persons();
            persons.AddRange(new[] { person1, person2, person3 });

            var meeting1 = new Meeting();
            meeting1.Persons = new List<Person> { person1 };
            meeting1.ResponsiblePerson = person1.Name;

            var meeting2 = new Meeting();
            meeting2.Persons = new List<Person> { person1, person2 };
            meeting2.ResponsiblePerson = person2.Name;

            var meeting3 = new Meeting();
            meeting3.Persons = new List<Person> { person1, person2, person3 };
            meeting3.ResponsiblePerson = person3.Name;

            var meetings1 = new Meetings();
            meetings1.AddRange(new[] { meeting1 });

            var meetings2 = new Meetings();
            meetings2.AddRange(new[] { meeting1, meeting2 });

            var meetings3 = new Meetings();
            meetings3.AddRange(new[] { meeting1, meeting2, meeting3 });

            var filter = new FilterData();

            var result = filter.FilterByResponsiblePerson("Tom", meetings3, persons);
            var expected = meetings1;

            Assert.Equal(expected, result);

        }
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

            var result = filter.FilterByNumberOfAttendees((0, 1), meetings3);
            var expected = meetings3;

            Assert.True(result.Contains(meeting1));
        }
    }
}