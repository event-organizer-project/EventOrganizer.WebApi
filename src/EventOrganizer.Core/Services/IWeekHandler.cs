using EventOrganizer.Core.DTO;

namespace EventOrganizer.Core.Services
{
    public interface IWeekHandler
    {
        WeeklyScheduleDTO GetCurrentWeek();
    }
}
