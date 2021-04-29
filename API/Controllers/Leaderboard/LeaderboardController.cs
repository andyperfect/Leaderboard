﻿using API.Authorization;
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
        public ActionResult Create([FromBody] CreateModel model)
        {
            var actor = Request.AuthorizeSiteAdministrator();
            var leaderboard = _leaderboardService.Create(model.Title, actor);
            return new OkObjectResult(leaderboard);
        }
    }
}