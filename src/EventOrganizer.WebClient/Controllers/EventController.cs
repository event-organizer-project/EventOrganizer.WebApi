using EventOrganizer.Core.Commands;
using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.Infrastructure;
using EventOrganizer.Core.Queries;
using EventOrganizer.Core.Queries.EventQueries;
using EventOrganizer.Domain.Models;
using EventOrganizer.WebClient.ModelMappers;
using EventOrganizer.WebClient.Views;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EventOrganizer.WebClient.Controllers
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
        // GET: api/<EventController>
        [HttpGet]
        public ActionResult<IList<EventPreviewModel>> Get(EventListSettings eventListSettings)
        {
            var parametrs = new GetEventListQueryParamters {  EventListSettings = eventListSettings };
            var result = getEventListQuery.Execute(parametrs);

            var eventList = EventMapper.MapModelListToPreviewList(result);
            return Ok(eventList);
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public ActionResult<EventViewModel> Get(int id)
        {
            return Ok(new EventViewModel { Id = id });
        }

        // POST api/<EventController>
        [HttpPost]
        public ActionResult<EventViewModel> Post([FromBody] EventViewModel eventView)
        {
            var eventModel = EventMapper.MapViewToModel(eventView);
            var parameters = new CreateEventCommandParameters { EventModel = eventModel };
            var result = createEventCommand.Execute(parameters);

            var createdEvent = EventMapper.MapModelToView(result);
            return Ok(createdEvent);
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public ActionResult<EventModel> Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
