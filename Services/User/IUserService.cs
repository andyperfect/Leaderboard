using Services.User.Models;
using Services.User.Roles;

namespace Services.User
{
    public interface IUserService
    {
        UserModel CreateUser(string email, string username, string password);
        string GetAccessToken(string username, string password);
        FullUser GetUserByAccessToken(string accessToken);
        FullUser GetUserById(long id);
        void GiveUserSiteRole(long id, SiteRoleType type);
    }
}
