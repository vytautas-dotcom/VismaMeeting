using Xunit;
using VismaMeeting_v2.Models;
using VismaMeeting_v2.Services.DataDisplay;
using System.Collections.Generic;
using System;

namespace VismaMeeting_v2.Test
{
    public class FilterDataTest
    {
        
        [Fact]
        public void ShouldReturnMeeting1WhenGivenResponsiblePersonName_Tom()
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
        public void ShouldReturnMeeting1andMeeting2WhenGivenInterval_2_0()
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

            var result = filter.FilterByNumberOfAttendees((2, 0), meetings3);
            var expected = meetings2;

            Assert.Equal(expected, result);
        }
        [Fact]
        public void ShouldReturnMeeting1andMeeting3WhenGivenDescription_aa()
        {
            var person1 = new Person() { Name = "Tom" };
            var person2 = new Person() { Name = "John" };
            var person3 = new Person() { Name = "Tim" };

            var persons = new Persons();
            persons.AddRange(new[] { person1, person2, person3 });

            var meeting1 = new Meeting();
            meeting1.Persons = new List<Person> { person1 };
            meeting1.Description = "aa";

            var meeting2 = new Meeting();
            meeting2.Persons = new List<Person> { person1, person2 };
            meeting2.Description = "bb";

            var meeting3 = new Meeting();
            meeting3.Persons = new List<Person> { person1, person2, person3 };
            meeting3.Description = "bbaa";

            var meetings1 = new Meetings();
            meetings1.AddRange(new[] { meeting1 });

            var meetings2 = new Meetings();
            meetings2.AddRange(new[] { meeting1, meeting2 });

            var meetings3 = new Meetings();
            meetings3.AddRange(new[] { meeting1, meeting2, meeting3 });

            var filter = new FilterData();

            var result = filter.FilterByDescription("aa", meetings3);
            var expected = new[] {meeting1, meeting3};

            Assert.Equal(expected, result);
        }
        [Fact]
        public void ShouldReturnMeetings3WhenGivenNumber_1()
        {
            var person1 = new Person() { Name = "Tom" };
            var person2 = new Person() { Name = "John" };
            var person3 = new Person() { Name = "Tim" };

            var persons = new Persons();
            persons.AddRange(new[] { person1, person2, person3 });

            var meeting1 = new Meeting();
            meeting1.Persons = new List<Person> { person1 };
            meeting1.Type = MeetType.Live;

            var meeting2 = new Meeting();
            meeting2.Persons = new List<Person> { person1, person2 };
            meeting2.Type = MeetType.Live;

            var meeting3 = new Meeting();
            meeting3.Persons = new List<Person> { person1, person2, person3 };
            meeting3.Type = MeetType.Live;

            var meetings1 = new Meetings();
            meetings1.AddRange(new[] { meeting1 });

            var meetings2 = new Meetings();
            meetings2.AddRange(new[] { meeting1, meeting2 });

            var meetings3 = new Meetings();
            meetings3.AddRange(new[] { meeting1, meeting2, meeting3 });

            var filter = new FilterData();

            var result = filter.FilterByType(1, meetings3);
            var expected = new[] { meeting1, meeting2, meeting3 };

            Assert.Equal(expected, result);
        }
        [Fact]
        public void ShouldReturnMeeting2andMeeting3WhenGivenNumber_1()
        {
            var person1 = new Person() { Name = "Tom" };
            var person2 = new Person() { Name = "John" };
            var person3 = new Person() { Name = "Tim" };

            var persons = new Persons();
            persons.AddRange(new[] { person1, person2, person3 });

            var meeting1 = new Meeting();
            meeting1.Persons = new List<Person> { person1 };
            meeting1.Category = MeetCategory.TeamBuilding;

            var meeting2 = new Meeting();
            meeting2.Persons = new List<Person> { person1, person2 };
            meeting2.Category = MeetCategory.CodeMonkey;

            var meeting3 = new Meeting();
            meeting3.Persons = new List<Person> { person1, person2, person3 };
            meeting3.Category = MeetCategory.CodeMonkey;

            var meetings1 = new Meetings();
            meetings1.AddRange(new[] { meeting1 });

            var meetings2 = new Meetings();
            meetings2.AddRange(new[] { meeting1, meeting2 });

            var meetings3 = new Meetings();
            meetings3.AddRange(new[] { meeting1, meeting2, meeting3 });

            var filter = new FilterData();

            var result = filter.FilterByCategory(1, meetings3);
            var expected = new[] {meeting2, meeting3};

            Assert.Equal(expected, result);
        }
        [Fact]
        public void ShouldReturnMeeting2WhenGivenNumber_05_22()
        {
            var person1 = new Person() { Name = "Tom" };
            var person2 = new Person() { Name = "John" };
            var person3 = new Person() { Name = "Tim" };

            var persons = new Persons();
            persons.AddRange(new[] { person1, person2, person3 });

            var meeting1 = new Meeting();
            meeting1.Persons = new List<Person> { person1 };
            meeting1.StartDate = DateTime.Parse("05/18/2022");
            meeting1.EndDate = DateTime.Parse("05/20/2022");

            var meeting2 = new Meeting();
            meeting2.Persons = new List<Person> { person1, person2 };
            meeting2.StartDate = DateTime.Parse("05/22/2022");
            meeting2.EndDate = DateTime.Parse("05/24/2022");

            var meeting3 = new Meeting();
            meeting3.Persons = new List<Person> { person1, person2, person3 };
            meeting3.StartDate = DateTime.Parse("06/11/2022");
            meeting3.EndDate = DateTime.Parse("06/12/2022");

            var meetings1 = new Meetings();
            meetings1.AddRange(new[] { meeting1 });

            var meetings2 = new Meetings();
            meetings2.AddRange(new[] { meeting1, meeting2 });

            var meetings3 = new Meetings();
            meetings3.AddRange(new[] { meeting1, meeting2, meeting3 });

            var filter = new FilterData();

            var result = filter.FilterByDate(DateTime.Parse("05/22/2022"), meetings3);
            var expected = new[] { meeting2 };

            Assert.Equal(expected, result);
        }
    }
}