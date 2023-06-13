using EventOrganizer.Core.Commands;
using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Queries;
using EventOrganizer.Core.Queries.EventQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace EventOrganizer.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IQuery<GetEventListQueryParameters, IList<EventDTO>> getEventListQuery;
        private readonly IQuery<GetEventByIdQueryParameters, EventDetailDTO> getEventByIdQuery;
        private readonly ICommand<CreateEventCommandParameters, EventDetailDTO> createEventCommand;
        private readonly ICommand<UpdateEventCommandParameters, EventDetailDTO> updateEventCommand;
        private readonly ICommand<DeleteEventCommandParameters, VoidResult> deleteEventCommand;
        private readonly ICommand<ScheduleEventCommandParameters, EventDetailDTO> joinEventCommand;
        private readonly IQuery<VoidParameters, UserOwnEventsDTO> getCurrentUserOwnEventsQuery;

        public EventController(
            IQuery<GetEventListQueryParameters, IList<EventDTO>> getEventListQuery,
            IQuery<GetEventByIdQueryParameters, EventDetailDTO> getEventByIdQuery,
            ICommand<CreateEventCommandParameters, EventDetailDTO> createEventCommand,
            ICommand<UpdateEventCommandParameters, EventDetailDTO> updateEventCommand,
            ICommand<DeleteEventCommandParameters, VoidResult> deleteEventCommand,
            ICommand<ScheduleEventCommandParameters, EventDetailDTO> joinEventCommand,
            IQuery<VoidParameters, UserOwnEventsDTO> getCurrentUserOwnEventsQuery)
        {
            this.getEventListQuery = getEventListQuery
                ?? throw new ArgumentNullException(nameof(getEventListQuery));
            this.getEventByIdQuery = getEventByIdQuery
                ?? throw new ArgumentNullException(nameof(getEventByIdQuery));
            this.createEventCommand = createEventCommand
                ?? throw new ArgumentNullException(nameof(createEventCommand));
            this.updateEventCommand = updateEventCommand
                ?? throw new ArgumentNullException(nameof(updateEventCommand));
            this.deleteEventCommand = deleteEventCommand
                ?? throw new ArgumentNullException(nameof(deleteEventCommand));
            this.joinEventCommand = joinEventCommand
                ?? throw new ArgumentNullException(nameof(joinEventCommand));
            this.getCurrentUserOwnEventsQuery = getCurrentUserOwnEventsQuery
                ?? throw new ArgumentNullException(nameof(getCurrentUserOwnEventsQuery));
        }

        /// <summary>
        /// Returns a list of events according to the given criteria
        /// </summary>
        /// <param name="filter">Event filter</param>
        /// <param name="top">Event top count</param>
        /// <param name="skip">Event skip count</param>
        /// <returns>Event List</returns>
        [HttpGet]
        public ActionResult<IList<EventDTO>> Get(string? filter = null, int top = 20, int skip = 0)
        {
            var result = getEventListQuery.Execute(new GetEventListQueryParameters { 
                Filter = filter,
                Top = top,
                Skip = skip
            });

            return Ok(result);
        }

        /// <summary>
        /// Returns an event for the given id.
        /// </summary>
        /// <param name="id">Event identifier</param>
        /// <returns>Event</returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public ActionResult<EventDetailDTO> Get(int id)
        {
            var result = getEventByIdQuery.Execute(new GetEventByIdQueryParameters { Id = id });

            return Ok(result);
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
        public ActionResult<EventDetailDTO> Post([FromBody] EventDetailDTO eventView)
        {
            var result = createEventCommand.
                Execute(new CreateEventCommandParameters { EventDetailDTO = eventView });

            return Ok(result);
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
        public ActionResult<EventDetailDTO> Put(EventDetailDTO eventView)
        {
            var result = updateEventCommand.
                Execute(new UpdateEventCommandParameters { EventDetailDTO = eventView });

            return Ok(result);
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
            deleteEventCommand.Execute(new DeleteEventCommandParameters { EventId = id });

            return Ok();
        }

        /// <summary>
        /// Schedule or unschedule some event by id
        /// </summary>
        /// <param name="id">Event identifier</param>
        /// <param name="isScheduled">Event schedule option</param>
        /// <returns>Event</returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("schedule/{id}/{isScheduled}")]
        public ActionResult<EventDetailDTO> Schedule(int id, bool isScheduled)
        {
            var result = joinEventCommand.Execute(new ScheduleEventCommandParameters 
            { 
                EventId = id,
                IsEventScheduled = isScheduled
            });

            return Ok(result);
        }

        /// <summary>
        /// Schedule or unschedule some event by id
        /// </summary>
        /// <returns>Events</returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("own-events")]
        public ActionResult<UserOwnEventsDTO> GetUserOwnEvents()
        {
            var result = getCurrentUserOwnEventsQuery.Execute(new VoidParameters());

            return Ok(result);
        }
    }
}
