using System.Collections.Generic;
using Services.User.Roles;

namespace Services.User.Models
{
    public class FullUser
    {
        public FullUser()
        {
            SiteRoles = new List<SiteRoleType>();
            GameRoles = new List<GameRole>();
        }

        public UserModel User { get; set; }
        public List<SiteRoleType> SiteRoles { get; set; }
        public List<GameRole> GameRoles { get; set; }
    }
}
