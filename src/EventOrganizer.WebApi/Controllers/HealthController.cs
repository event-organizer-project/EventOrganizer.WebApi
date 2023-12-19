using EventOrganizer.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventOrganizer.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService healthService;

        public HealthController(IHealthService healthService)
        {
            this.healthService = healthService ?? throw new ArgumentNullException(nameof(healthService));
        }

        // GET: api/<HealthController>
        [HttpGet]
        public string Get()
        {
            var dbConnectionState = healthService.IsDbConnectionHealthy()
                ? "Healthy"
                : "Unhealthy";

            return $"Service: Running\nDB Connection: {dbConnectionState}";
        }
    }
}
