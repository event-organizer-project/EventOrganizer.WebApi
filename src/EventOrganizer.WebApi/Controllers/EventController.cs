using EventOrganizer.Core.Commands;
using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.Queries;
using EventOrganizer.Core.Queries.EventQueries;
using EventOrganizer.Domain.Models;
using EventOrganizer.WebApi.ModelMappers;
using EventOrganizer.WebApi.Views;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace EventOrganizer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private ICommand<CreateEventCommandParameters, EventModel> createEventCommand;
        private IQuery<GetEventListQueryParamters, IList<EventModel>> getEventListQuery;

        public EventController(ICommand<CreateEventCommandParameters, EventModel> createEventCommand,
            IQuery<GetEventListQueryParamters, IList<EventModel>> getEventListQuery)
        {
            this.createEventCommand = createEventCommand
                ?? throw new ArgumentNullException(nameof(createEventCommand));
            this.getEventListQuery = getEventListQuery
                    ?? throw new ArgumentNullException(nameof(getEventListQuery));
        }

        /// <summary>
        /// Returns a list of events according to the given criteria
        /// </summary>
        /// <param name="filter">Event filter</param>
        /// <returns>Event List</returns>
        [HttpGet]
        public ActionResult<IList<EventPreviewModel>> Get(string filter)
        {
            var parametrs = new GetEventListQueryParamters();
            var result = getEventListQuery.Execute(parametrs);

            var eventList = EventMapper.MapModelListToPreviewList(result);
            return Ok(eventList);
        }

        /// <summary>
        /// Returns an event for the given id.
        /// </summary>
        /// <param name="id">Event identifier</param>
        /// <returns>Event</returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public ActionResult<EventViewModel> Get(int id)
        {
            var eventView = new EventViewModel
            {
                Id = id,
                Title = $"Event number {id}",
                Description = $"Description for event number {id}"
            };

            return Ok(eventView);
        }

        /// <summary>
        /// Creates an event.
        /// </summary>
        /// <param name="eventView"></param>
        /// <returns>A newly created event</returns>
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        [HttpPost]
        public ActionResult<EventViewModel> Post([FromBody] EventViewModel eventView)
        {
            var eventModel = EventMapper.MapViewToModel(eventView);
            var parameters = new CreateEventCommandParameters { EventModel = eventModel };
            var result = createEventCommand.Execute(parameters);

            var createdEvent = EventMapper.MapModelToView(result);
            return Ok(createdEvent);
        }

        /// <summary>
        /// Updates an event.
        /// </summary>
        /// <param name="eventView"></param>
        /// <returns>An updated event</returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]
        [HttpPut]
        public ActionResult<EventViewModel> Put([FromBody] EventViewModel eventView)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a specific event for the given id.
        /// </summary>
        /// <param name="id">Event identifier</param>
        /// <returns></returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
