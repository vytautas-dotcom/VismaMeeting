using VismaMeeting_v2.Services.DataForMessages;
using VismaMeeting_v2.Services.Messages;
using VismaMeeting_v2.Extensions;
using VismaMeeting_v2.Models;

namespace VismaMeeting_v2.Services.Checking
{
    public class DataChecking
    {
        private readonly UIMessages _uIMessages;
        private readonly MessagesData _messageData;
        public DataChecking(UIMessages uIMessages)
        {
            _uIMessages = uIMessages;
            _messageData = new MessagesData();
        }
        public bool IsInputNotNullOrEmptySpace(string value)
        {
            if(string.IsNullOrEmpty(value))
                return false;
            return true;
        }
        public bool IsInputNumber(string value, out int input)
        {
            bool success = int.TryParse(value, out input);
            bool isInputCorrect = IsInputNotNullOrEmptySpace(value) && success;
            return isInputCorrect;
        }
        public bool IsInputDate(string value, out DateTime dateOut)
        {
            DateTime dateTime;
            bool isDateCorrect = DateTime.TryParse(value, out DateTime date);
            dateTime = date;
            dateOut = dateTime;
            return isDateCorrect && !date.IsEmpty();
        }
        public bool IsSelectedIndexNotOutTheRange<T>(int index, List<T> list)
            => index <= list.Count - 1 && index >= 0;
        public bool IsConfimationSuccessful(string input)
        {
            char letter = 'y';
            bool isInputCorrect = char.TryParse(input, out char confirmationLetter);
            if (isInputCorrect && letter == confirmationLetter)
                return true;
            return false;
        }

        public bool IsListNotEmptyAndNotNull<T>(List<T> list)
            => list != null && list.Count > 0;

        public bool IsPersonResponsibleForMeeting(Person person, Meeting meeting)
            => meeting.ResponsiblePerson == person.Name;

        public bool IsPersonAddedToMeeting(Person person, Meeting meeting)
        {
            bool isAdded = false;
            foreach (var item in meeting.Persons)
            {
                if (item.Id == person.Id)
                    isAdded = true;
            }
            return isAdded;
        }
        public bool IsMeetingPersonsListFull(Persons persons, Meeting meeting)
            => meeting.Persons.Count == persons.Count;

        public bool ExistMeetingThatPersonIsResponsibleFor(Meetings meetings, Person person)
        {
            foreach (var meeting in meetings)
            {
                if (IsPersonResponsibleForMeeting(person, meeting))
                    return true;
            }
            return false;
        }

        public bool HasPersonMeeting(Person person, Meeting meeting)
            => person.PersonMeetings.ContainsKey(meeting.Id);

        public bool HasMeetingPerson(Meeting meeting, Person person)
        {
            foreach (var item in meeting.Persons)
                if (item.Id == person.Id)
                    return true;
            return false;
        }

        public bool IsMeetingToDeleteForPerson(Meetings meetingList, Person person)
        {
            bool isToDelete = false;
            foreach (var meeting in meetingList)
            {
                if (IsPersonResponsibleForMeeting(person, meeting))
                    isToDelete = true;
            }
            return isToDelete;
        }
        public bool IsMeetigToDeleteForPerson(Meetings meetings, Person person, int index)
        {
            if (!IsSelectedIndexNotOutTheRange(index, meetings))
            {
                _uIMessages.WarningMessage(_messageData.WarningMessages["InputWarning"]);
                return false;
            }
            else if(!IsPersonResponsibleForMeeting(person, meetings[index]))
            {
                _uIMessages.WarningMessage(_messageData.WarningMessages["MeetingDeleteWarning"]);
                return false;
            }
            return true;
        }
    }
}
