namespace Services.Leaderboard.Models
{
    public class LeaderboardModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long DateCreated { get; set; }
        public long AddedBy { get; set; }
    }
}
