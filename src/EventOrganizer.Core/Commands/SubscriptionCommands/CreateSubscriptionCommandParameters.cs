namespace EventOrganizer.Core.Commands.SubscriptionCommands
{
    public class CreateSubscriptionCommandParameters
    {
        public string Endpoint { get; set; }

        public string P256DH { get; set; }

        public string Auth { get; set; }

        public CreateSubscriptionCommandParameters(string endpoint, string p256dh, string auth)
        {
            Endpoint = endpoint;
            P256DH = p256dh;
            Auth = auth;
        }
    }
}
