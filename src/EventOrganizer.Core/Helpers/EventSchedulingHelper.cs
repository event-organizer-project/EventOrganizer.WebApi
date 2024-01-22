namespace EventOrganizer.Core.Helpers
{
    internal class EventSchedulingHelper
    {
        public static bool IsDateTheRestOfToday(DateTimeOffset date)
            => date > DateTimeOffset.UtcNow && date < DateTimeOffset.UtcNow.Date.AddDays(1);
    }
}
