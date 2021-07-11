using API.Authorization;
using API.Controllers.Filters;
using API.Controllers.Leaderboard.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Leaderboard;
using Services.Windsor;

namespace API.Controllers.Leaderboard
{
    [ApiController]
    [StandardExceptionFilter]
    [Route("Leaderboard")]
    public class LeaderboardController : Controller
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController()
        {
            _leaderboardService = IoC.Container.Resolve<ILeaderboardService>();
        }

        [HttpPost, Route("")]
        public ActionResult Create([FromBody] LeaderboardCreateModel model)
        {
            var actor = Request.AuthorizeSiteAdministrator();
            var leaderboard = _leaderboardService.Create(model.Title, actor);
            return new OkObjectResult(leaderboard);
        }

        [HttpGet, Route("{id:long}")]
        public ActionResult Get(long id)
        {
            var leaderboard = _leaderboardService.Get(id);
            if (leaderboard == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(leaderboard);
        }

        [HttpPut, Route("{id:long}")]
        public ActionResult Update(long id, [FromBody] LeaderboardUpdateModel model)
        {
            var actor = Request.AuthorizeLeaderboardEdit(id);
            var leaderboard = _leaderboardService.UpdateTitle(id, model.Title);
            return new OkObjectResult(leaderboard);
        }
    }
}
