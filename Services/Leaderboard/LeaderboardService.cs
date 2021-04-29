﻿using System;
using Services.Leaderboard.Models;
using Services.User.Models;

namespace Services.Leaderboard
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ILeaderboardRepository _leaderboardRepository;
        
        public LeaderboardService(ILeaderboardRepository leaderboardRepository)
        {
            _leaderboardRepository = leaderboardRepository;
        }
        
        public LeaderboardModel Create(string title, FullUser actor)
        {
            var model = new LeaderboardModel
            {
                Title = title,
                DateCreated = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                AddedBy = actor.User.Id
            };
            _leaderboardRepository.Create(model);
            return model;
        }

        public LeaderboardModel Get(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
