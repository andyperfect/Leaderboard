using System.Collections.Generic;

namespace Services.User.Roles
{
    public class UserRoles
    {
        public UserRoles()
        {
            SiteRoles = new List<SiteRoleType>();
            LeaderboardRoles = new List<LeaderboardRole>();
        }

        public IEnumerable<SiteRoleType> SiteRoles { get; set; }
        public IEnumerable<LeaderboardRole> LeaderboardRoles { get; set; }
    }
}
