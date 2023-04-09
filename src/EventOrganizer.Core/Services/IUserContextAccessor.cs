using System.Security.Claims;

namespace EventOrganizer.Core.Services
{
    public interface IUserContextAccessor
    {
        int GetUserId();

        ClaimsPrincipal GetUserContext();
    }
}
