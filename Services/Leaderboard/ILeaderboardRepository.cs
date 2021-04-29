using Services.Leaderboard.Models;

namespace Services.Leaderboard
{
    public interface ILeaderboardRepository
    {
        public void Create(LeaderboardModel leaderboard);
    }
}
