namespace EventOrganizer.Core.Queries.CalendarQueries
{
    public class GetWeeklyScheduleQueryParameters
    {
        public GetWeeklyScheduleQueryParameters(int offset)
        {
            Offset = offset;
        }

        public int Offset {  get; set; }
    }
}
