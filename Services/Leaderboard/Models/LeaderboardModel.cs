using System;
using System.Diagnostics.CodeAnalysis;

namespace Services.Leaderboard.Models
{
    public class LeaderboardModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long DateCreated { get; set; }
        public long AddedBy { get; set; }

        public override string ToString()
        {
            return $"Leaderboard: {Id},{Title},{DateCreated},{AddedBy}";
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, DateCreated, AddedBy);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LeaderboardModel);
        }

        private bool Equals(LeaderboardModel other)
        {
            return
                other != null &&
                this.Id == other.Id &&
                this.Title == other.Title &&
                this.DateCreated == other.DateCreated &&
                this.AddedBy == other.AddedBy;
        }
    }
}
