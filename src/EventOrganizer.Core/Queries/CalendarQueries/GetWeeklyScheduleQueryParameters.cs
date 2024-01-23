namespace EventOrganizer.Core.Queries.CalendarQueries
{
    public class GetWeeklyScheduleQueryParameters
    {
        public GetWeeklyScheduleQueryParameters(int weekOffset, int timeZoneOffsetInMinutes = 0)
        {
            WeekOffset = weekOffset;
            TimeZoneOffsetInMinutes = timeZoneOffsetInMinutes;
        }

        public int WeekOffset {  get; set; }
        public int TimeZoneOffsetInMinutes { get; set; }

    }
}
