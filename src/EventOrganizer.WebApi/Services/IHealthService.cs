namespace EventOrganizer.WebApi.Services
{
    public interface IHealthService
    {
        bool IsDbConnectionHealthy();
    }
}
