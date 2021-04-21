using System;
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
    }
}
