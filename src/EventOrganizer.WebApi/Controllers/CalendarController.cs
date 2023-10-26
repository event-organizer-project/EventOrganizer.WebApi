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
        /// Returns current user profile details.
        /// </summary>
        /// <returns>Event</returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpGet("current")]
        public ActionResult<UserDTO> Get()
        {
            var result = qetWeeklyScheduleQuery.Execute(new GetWeeklyScheduleQueryParameters());

            return Ok(result);
        }
    }
}
