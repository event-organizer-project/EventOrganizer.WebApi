using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.EF.MySql.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly EventOrganazerDbContext dbContext;

        public LogRepository(EventOrganazerDbContext eventOrganazerDbContext)
        {
            dbContext = eventOrganazerDbContext
                ?? throw new ArgumentNullException(nameof(eventOrganazerDbContext));
        }

        public void SaveLog(LogRecord logRecord)
        {
            dbContext.LogRecords.Add(logRecord);

            dbContext.SaveChanges();
        }
    }
}
