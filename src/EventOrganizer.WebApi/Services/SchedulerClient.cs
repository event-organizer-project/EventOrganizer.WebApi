using EventOrganizer.Core.Services;
using EventOrganizer.WebApi.Infrastructure;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Security;

namespace EventOrganizer.WebApi.Services
{
    public class SchedulerClient : ISchedulerClient
    {
        private readonly ILogger<SchedulerClient> logger;

        private static readonly HttpClient client = new();

        private readonly ClientCredentialsTokenRequest tokenRequest;

        private readonly string endpoint;

        public SchedulerClient(IOptions<WebOptions> options, ILogger<SchedulerClient> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var webOptions = options.Value;

            tokenRequest = new ClientCredentialsTokenRequest
            {
                Address = $"{webOptions.Authority}/connect/token",
                ClientId = webOptions.WebApiName,
                Scope = webOptions.SchedulerApiName
            };

            endpoint = webOptions.SchedulerClient;
        }

        public async Task AddEventToSchedule(int eventId, int userId)
        {
            await Request(() => client.PostAsync(GetEndpoint(eventId, userId), null));
        }

        public async Task RemoveEventFromSchedule(int eventId,params int[] userIds)
        {
            await Request(() => client.DeleteAsync(GetEndpoint(eventId, userIds)));
        }

        private async Task Request(Func<Task<HttpResponseMessage>> request)
        {
            try
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(tokenRequest);

                if (tokenResponse.IsError)
                {
                    // TO DO: replace with custom exception classes
                    throw new SecurityException("Token request failed");
                }

                client.SetBearerToken(tokenResponse.AccessToken);

                var response = await request();
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException exception)
            {
                logger.LogError(exception, "SchedulerClient request failed.");
            }
            catch (SecurityException)
            {
                // TO DO: Add logger
            }
        }

        private string GetEndpoint(int eventId,params int[] userIds) =>
            $"{endpoint}/scheduler/{eventId}/{string.Join(',', userIds)}";
    }
}
