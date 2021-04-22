using API.Authorization;
using API.Controllers.Filters;
using API.Controllers.User.Models;
using Microsoft.AspNetCore.Mvc;
using Services.User;
using Services.Windsor;

namespace API.Controllers.User
{
    [ApiController]
    [StandardExceptionFilter]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController()
        {
            _userService = IoC.Container.Resolve<IUserService>();
        }


        [HttpPost, Route("")]
        public ActionResult Create([FromBody] CreateModel model)
        {
            Request.AuthorizeSiteAdministrator();
            var user = _userService.CreateUser(model.Email, model.Username, model.Password);
            return new OkObjectResult(user);
        }

        [HttpGet, Route("{id}")]
        public ActionResult Get(long id)
        {
            var user = _userService.GetUserById(id);
            return new OkObjectResult(user);
        }

        [HttpPost, Route("{login}")]
        public ActionResult Login([FromBody] LoginModel model)
        {
            var accessToken = _userService.GetAccessToken(model.Username, model.Password);
            return new OkObjectResult(new {accessToken});
        }
    }
}
