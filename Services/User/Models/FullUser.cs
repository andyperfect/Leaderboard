using System.Collections.Generic;
using Services.User.Roles;

namespace Services.User.Models
{
    public class FullUser
    {
        public FullUser()
        {
            SiteRoles = new List<SiteRoleType>();
            LeaderboardRoles = new List<LeaderboardRole>();
        }

        public UserModel User { get; set; }
        public List<SiteRoleType> SiteRoles { get; set; }
        public List<LeaderboardRole> LeaderboardRoles { get; set; }
    }
}
