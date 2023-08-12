using EventOrganizer.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using EventOrganizer.Core.Queries;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace EventOrganizer.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class UserProfileController : ControllerBase
    {
        private readonly IQuery<VoidParameters, UserDTO> getCurrentUserQuery;

        public UserProfileController(IQuery<VoidParameters, UserDTO> getCurrentUserQuery)
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
            var result = getCurrentUserQuery.Execute(new VoidParameters());

            return Ok(result);
        }
    }
}
