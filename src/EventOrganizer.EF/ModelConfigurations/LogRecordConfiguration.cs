using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF.EntityConfigurations
{
    public class LogRecordConfiguration : IEntityTypeConfiguration<LogRecord>
    {
        public void Configure(EntityTypeBuilder<LogRecord> builder)
        {
            builder.Property(lr => lr.LogLevel).HasMaxLength(30);
            builder.Property(lr => lr.CallerName).HasMaxLength(300);
        }
    }
}
