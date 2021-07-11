using Services.Leaderboard.Models;
using Services.User.Models;

namespace Services.Leaderboard
{
    public interface ILeaderboardService
    {
        public LeaderboardModel Create(string title, FullUser actor);
        public LeaderboardModel Get(long id);
        public LeaderboardModel UpdateTitle(long id, string title);
    }
}
