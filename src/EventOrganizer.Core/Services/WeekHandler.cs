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

        private const int weekDaysCount = 7;

        public WeeklyScheduleDTO GetCurrentWeek()
        {
            var week = new WeeklyScheduleDTO
            {
                WeekDays = new DaylyScheduleDTO[weekDaysCount]
            };

            var currentDate = DateTime.Today;
            var currentDayOfWeek = currentDate.DayOfWeek;

            var startOfWeek = currentDayOfWeek != DayOfWeek.Sunday 
                ? currentDate.AddDays(-(int)currentDayOfWeek + (int)DayOfWeek.Monday)
                : currentDate.AddDays(-6);

            for(int i = 0; i < weekDaysCount; i++)
            {
                week.WeekDays[i] = new DaylyScheduleDTO
                {
                    WeekDay = weekDays[i].ToString(),
                    Date = startOfWeek.AddDays(i)
                };
            }

            return week;
        }
    }
}
