using System;
using Services.Password;
using Services.User.Models;
using Services.User.Roles;
using Services.Windsor;

namespace Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService()
        {
            _userRepository = IoC.Container.Resolve<IUserRepository>();
        }
        
        public UserModel CreateUser(string email, string username, string password)
        {
            var salt = PasswordService.GenerateSalt();
            var hashedPassword = PasswordService.HashPassword(password, salt);
            var nowDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var user = new UserModel
            {
                Email = email,
                Username = username,
                DateCreated = nowDate
            };

            _userRepository.CreateUser(user, salt, hashedPassword, GenerateAccessToken(), nowDate);
            return user;
        }

        public string GetAccessToken(string username, string password)
        {
            var user = _userRepository.GetFullPasswordUser(username);
            if (user == null || !PasswordService.VerifyPassword(password, user.HashedPassword, user.Salt))
            {
                throw new Exception("Incorrect username and/or password");
            }

            return user.AccessToken;
        }

        public FullUser GetUserByAccessToken(string accessToken)
        {
            return _userRepository.GetUserByAccessToken(accessToken);
        }

        public FullUser GetUserById(long id)
        {
            return _userRepository.GetUserById(id);
        }

        public FullUser GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public void GiveUserSiteRole(long id, SiteRoleType type)
        {
            var user = GetUserById(id);
            if (user == null || user.SiteRoles.Contains(type))
            {
                return;
            }

            _userRepository.AddSiteRoleToUser(id, type);
        }
        
        private static string GenerateAccessToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
