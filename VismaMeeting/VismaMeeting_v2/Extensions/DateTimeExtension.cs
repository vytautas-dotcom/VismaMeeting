namespace VismaMeeting_v2.Extensions
{
    public static class DateTimeExtension
    {
        public static bool IsEmpty(this DateTime dateTime)
            => dateTime == default(DateTime);
    }
}
