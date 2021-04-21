using System.Collections.Generic;

namespace Services.User.Roles
{
    public class GameRole
    {
        public long GameId { get; set; }
        public List<GameRoleType> Roles { get; set; }
    }
}
