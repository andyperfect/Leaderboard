using System.Collections.Generic;

namespace Services.User.Roles
{
    public class UserRoles
    {
        public UserRoles()
        {
            SiteRoles = new List<SiteRoleType>();
            GameRoles = new List<GameRole>();
        }

        public IEnumerable<SiteRoleType> SiteRoles { get; set; }
        public IEnumerable<GameRole> GameRoles { get; set; }
    }
}
