using System;
using Microsoft.AspNetCore.Http;
using Services.User.Models;

namespace API.Authorization
{
    public static class AuthorizationHelpers
    {
        public const string NotAuthorizedError = "Not Authorized to perform this action";
        public static FullUser GetActor(HttpRequest request)
        {
            try
            {
                return (FullUser) request.HttpContext.Items["actor"];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
