using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizer.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IQuery<VoidParameters, IList<string>> getTagListQuery;

        public TagController(IQuery<VoidParameters, IList<string>> getTagListQuery)
        {
            this.getTagListQuery = getTagListQuery
                ?? throw new ArgumentNullException(nameof(getTagListQuery));
        }

        /// <summary>
        /// Returns a list of events tags
        /// </summary>
        /// <returns>Tag List</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return getTagListQuery.Execute(new VoidParameters());
        }
    }
}
