namespace EventOrganizer.WebApi.Models
{
    public class EventListSearchCriteria
    {
        public string? Filter { get; set; }
        public string[]? Tags { get; set; }
        public int Top { get; set; }
        public int Skip { get; set; }
        public DateTimeOffset? StartingFrom { get; set; }
        public DateTimeOffset? EndingBefore { get; set; }
        public bool OnlyForCurrentUser { get; set; }
    }
}
