using EventOrganizer.Core.Services;
using IdentityModel;
using System.Security;
using System.Security.Claims;

namespace EventOrganizer.WebApi.Services
{
    public class UserContextAccessor: IUserContextAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor 
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public int GetUserId()
        {
            var claim = GetUserContext().Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Id)?.Value;

            if (string.IsNullOrEmpty(claim))
                // TO DO: replace with custom exception classes
                throw new SecurityException("Claim does not exist");

            if (!int.TryParse(claim, out int userId))
                // TO DO: replace with custom exception classes
                throw new SecurityException("Incorrect user id claim format");

            return userId;
        }

        public ClaimsPrincipal GetUserContext()
        {
            if(httpContextAccessor.HttpContext == null)
                throw new SecurityException("HttpContext does not exist");

            return httpContextAccessor.HttpContext.User;
        }
    }
}
