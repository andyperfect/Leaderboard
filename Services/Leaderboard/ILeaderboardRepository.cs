using System.Collections.Generic;
using Services.Leaderboard.Models;

namespace Services.Leaderboard
{
    public interface ILeaderboardRepository
    {
        public void Create(LeaderboardModel leaderboard);
        LeaderboardModel Get(long id);
        bool UpdateTitle(long id, string title);
    }
}
