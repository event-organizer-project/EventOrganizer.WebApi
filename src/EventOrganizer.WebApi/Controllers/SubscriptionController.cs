using EventOrganizer.Core.Commands;
using EventOrganizer.Core.Commands.SubscriptionCommands;
using EventOrganizer.Core.DTO;
using EventOrganizer.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace EventOrganizer.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class SubscriptionController : ControllerBase
    {
        private readonly ICommand<CreateSubscriptionCommandParameters, VoidResult> createSubscriptionCommand;

        public SubscriptionController(ICommand<CreateSubscriptionCommandParameters, VoidResult> createSubscriptionCommand)
        {
            this.createSubscriptionCommand = createSubscriptionCommand
                ?? throw new ArgumentNullException(nameof(createSubscriptionCommand));
        }

        // POST api/<SubscriptionController>
        [HttpPost]
        public IActionResult Post([FromBody] Subcription subcription)
        {
            var parameters = new CreateSubscriptionCommandParameters(
                subcription.Endpoint, subcription.P256dh, subcription.Auth);
            
            createSubscriptionCommand.Execute(parameters);
            
            return Ok();
        }
    }
}
