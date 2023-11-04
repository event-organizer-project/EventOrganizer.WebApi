namespace EventOrganizer.WebApi.Infrastructure
{
    public class WebOptions
    {
        public string Authority { get; set; } = string.Empty;

        public string WebClient { get; set; } = string.Empty;

        public string SchedulerClient { get; set; } = string.Empty;

        public string WebApiName { get; set; } = string.Empty;

        public string SchedulerApiName { get; set; } = string.Empty;
    }
}
