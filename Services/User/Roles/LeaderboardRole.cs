using System.Collections.Generic;

namespace Services.User.Roles
{
    public class LeaderboardRole
    {
        public long LeaderboardId { get; set; }
        public List<LeaderboardRoleType> Roles { get; set; }
    }
}
