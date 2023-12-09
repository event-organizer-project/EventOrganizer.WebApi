using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Repositories
{
    public interface ILogRepository
    {
        void SaveLog(LogRecord logRecord);
    }
}
