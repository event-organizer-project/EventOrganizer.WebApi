using EventOrganizer.Core.DTO;

namespace EventOrganizer.Core.Services
{
    public class WeekHandler : IWeekHandler
    {
        private DayOfWeek[] weekDays = new DayOfWeek[] 
        { 
            DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday,
            DayOfWeek.Saturday, DayOfWeek.Sunday
        };

        private const int visibleWeekDaysCount = 7;
        private const int weekDaysCount = 7;

        public WeeklyScheduleDTO GetWeek(int weekOffset, int timeZoneOffsetInMinutes)
        {
            var week = new WeeklyScheduleDTO
            {
                WeekDays = new DaylyScheduleDTO[visibleWeekDaysCount]
            };
            var timeZoneOffset = TimeSpan.FromMinutes(timeZoneOffsetInMinutes);

            var currentDate = DateTimeOffset.UtcNow.ToOffset(timeZoneOffset).Date;
            var currentDayOfWeek = currentDate.DayOfWeek;

            var startOfWeek = currentDayOfWeek != DayOfWeek.Sunday
                ? currentDate.AddDays(-(int)currentDayOfWeek + (int)DayOfWeek.Monday + weekOffset * weekDaysCount)
                : currentDate.AddDays(1 - visibleWeekDaysCount + weekOffset * weekDaysCount);

            for (var i = 0; i < visibleWeekDaysCount; i++)
            {
                week.WeekDays[i] = new DaylyScheduleDTO
                {
                    WeekDay = weekDays[i].ToString(),
                    Date = new DateTimeOffset(startOfWeek.AddDays(i), timeZoneOffset)
                };
            }

            return week;
        }
    }
}
