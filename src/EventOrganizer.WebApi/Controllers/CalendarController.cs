using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Queries;
using EventOrganizer.Core.Queries.CalendarQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace EventOrganizer.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class CalendarController : ControllerBase
    {
        private readonly IQuery<GetWeeklyScheduleQueryParameters, WeeklyScheduleDTO> qetWeeklyScheduleQuery;

        public CalendarController(IQuery<GetWeeklyScheduleQueryParameters, WeeklyScheduleDTO> getCurrentUserQuery)
        {
            this.qetWeeklyScheduleQuery = getCurrentUserQuery
                ?? throw new ArgumentNullException(nameof(getCurrentUserQuery));
        }

        /// <summary>
        /// Returns weekly schedule
        /// </summary>
        /// <param name="weekOffset">Week offset</param>
        /// <returns>Weekly schedule</returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpGet("{weekOffset}")]
        public ActionResult<WeeklyScheduleDTO> Get(int weekOffset)
        {
            Request.Headers.TryGetValue("TimeZoneOffset", out var timeZoneHeaderValue);
            int.TryParse(timeZoneHeaderValue, out int timeZoneOffsetInMinutes);

            var result = qetWeeklyScheduleQuery.Execute(new GetWeeklyScheduleQueryParameters(weekOffset, timeZoneOffsetInMinutes));

            return Ok(result);
        }
    }
}
