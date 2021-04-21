using Microsoft.AspNetCore.Mvc.Filters;
using Services.User;
using Services.User.Models;
using Services.Windsor;

namespace API.Controllers.Filters
{
    public class ActorFilter : IActionFilter
    {
        private readonly IUserService _userService;
        public ActorFilter()
        {
            _userService = IoC.Container.Resolve<IUserService>();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            FullUser actor = null;
            var accessToken = context.HttpContext.Request.Headers["Access-Token"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                actor = _userService.GetUserByAccessToken(accessToken);
            }

            context.HttpContext.Items["actor"] = actor;
        }

        public void OnActionExecuted(ActionExecutedContext context)  
        {

        }
    }
}
