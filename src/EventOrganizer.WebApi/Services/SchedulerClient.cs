using EventOrganizer.Core.Services;
using EventOrganizer.WebApi.Infrastructure;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Text;

namespace EventOrganizer.WebApi.Services
{
    public class SchedulerClient : ISchedulerClient
    {
        private static readonly HttpClient client = new();

        private readonly WebOptions webOptions;

        public SchedulerClient(IOptions<WebOptions> options)
        {
            webOptions = options.Value;
        }

        public async Task AddEventToSchedule(int eventId, int userId)
        {
            try
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = $"{webOptions.Authority}/connect/token",
                    ClientId = webOptions.WebApiName,
                    Scope = webOptions.SchedulerApiName
                });

                if (tokenResponse.IsError)
                {
                    // TO DO: Add logger
                    return;
                }

                client.SetBearerToken(tokenResponse.AccessToken);

                var url = $"{webOptions.SchedulerClient}/scheduler/add-event";
                var data = new StringContent($"{{\"EventId\":\"{eventId}\", \"UserId\":\"{userId}\"}}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                // TO DO: Add logger
            }
        }

        public Task RemoveEventFromSchedule(int eventId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
