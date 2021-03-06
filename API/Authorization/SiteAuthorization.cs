using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Services.User.Models;
using Services.User.Roles;

namespace API.Authorization
{
    public static class SiteAuthorization
    {
        public static FullUser AuthorizeSiteAdministrator(this HttpRequest request)
        {
            var actor = AuthorizationHelpers.GetActor(request);
            if (actor?.SiteRoles == null || !actor.SiteRoles.Contains(SiteRoleType.Administrator))
            {
                throw new UnauthorizedAccessException(AuthorizationHelpers.NotAuthorizedError);
            }

            return actor;
        }
        
        public static FullUser AuthorizeLeaderboardEdit(this HttpRequest request, long leaderboardId)
        {
            var actor = AuthorizationHelpers.GetActor(request);
            var authorized =
                actor.LeaderboardRoles.Any(
                    r => r.LeaderboardId == leaderboardId && r.Roles.Contains(LeaderboardRoleType.Moderator))
                || actor.SiteRoles.Contains(SiteRoleType.Administrator);

            if (!authorized)
            {
                throw new UnauthorizedAccessException(AuthorizationHelpers.NotAuthorizedError);
            }

            return actor;
        }
    }
}
