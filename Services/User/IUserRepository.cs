using Services.User.Models;
using Services.User.Roles;

namespace Services.User
{
    public interface IUserRepository
    {
        /// <summary>
        /// Creates the user and updates the id of the model before returning
        /// </summary>
        /// <param name="user"></param>
        /// <param name="salt"></param>
        /// <param name="password"></param>
        /// <param name="accessToken"></param>
        /// <param name="accessTokenDate"></param>
        void CreateUser(
            UserModel user,
            string salt,
            string password,
            string accessToken,
            long accessTokenDate);

        FullUser GetUserByAccessToken(string accessToken);

        FullUser GetUserById(long id);

        FullUser GetUserByUsername(string username);

        UserPasswordModel GetFullPasswordUser(string username);

        void AddSiteRoleToUser(long userId, SiteRoleType type);


    }
}
