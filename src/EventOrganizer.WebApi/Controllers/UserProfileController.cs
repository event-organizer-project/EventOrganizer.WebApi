using EventOrganizer.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using EventOrganizer.Core.Queries.UserQueries;
using EventOrganizer.Core.Queries;
using Microsoft.AspNetCore.Authorization;

namespace EventOrganizer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IQuery<GetCurrentUserQueryParameters, UserDTO> getCurrentUserQuery;

        public UserProfileController(IQuery<GetCurrentUserQueryParameters, UserDTO> getCurrentUserQuery)
        {
            this.getCurrentUserQuery = getCurrentUserQuery
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
            var result = getCurrentUserQuery.Execute(new GetCurrentUserQueryParameters());

            return Ok(result);
        }
    }
}
