using EventOrganizer.Utils.Logging;

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

        public Task SaveLog(LogRecord logRecord)
        {
            dbContext.LogRecords.Add(Map(logRecord));

            dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        // TO DO: Change this logic
        public static Domain.Models.LogRecord Map(LogRecord logRecord)
        {
            return new Domain.Models.LogRecord
            {
                Id = logRecord.Id,
                LogLevel = logRecord.LogLevel,
                StackTrace = logRecord.StackTrace,
                Message = logRecord.Message,
                ExceptionMessage = logRecord.ExceptionMessage,
                AdditionalInfo = logRecord.AdditionalInfo,
                CallerName = logRecord.CallerName,
                Application = logRecord.Application,
                CreatedAt = logRecord.CreatedAt.DateTime
            };
        }
    }
}
