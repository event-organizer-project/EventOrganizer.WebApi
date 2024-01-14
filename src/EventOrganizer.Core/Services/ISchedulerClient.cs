namespace EventOrganizer.Core.Services
{
    public interface ISchedulerClient
    {
        Task AddEventToSchedule(int eventId, int userId);

        Task RemoveEventFromSchedule(int eventId, params int[] userIds);
    }
}
