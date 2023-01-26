namespace EventOrganizer.Core.Queries.EventQueries
{
    public class GetEventListQueryParameters
    {
        public string? Filter { get; set; }

        public IList<string> Tags { get; set; }

        public int Top { get; set; }

        public int Skip { get; set; }
    }
}
