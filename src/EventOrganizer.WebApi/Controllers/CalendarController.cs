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
        /// <param name="offset">Week offset</param>
        /// <returns>Weekly schedule</returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpGet("{offset}")]
        public ActionResult<WeeklyScheduleDTO> Get(int offset)
        {
            var result = qetWeeklyScheduleQuery.Execute(new GetWeeklyScheduleQueryParameters(offset));

            return Ok(result);
        }
    }
}
