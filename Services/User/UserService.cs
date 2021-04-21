using System;
using Services.Password;
using Services.User.Models;
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

        public FullUser GetUserByAccessToken(string accessToken)
        {
            return _userRepository.GetUserByAccessToken(accessToken);
        }

        public FullUser GetUserById(long id)
        {
            return _userRepository.GetUserById(id);
        }
        
        private static string GenerateAccessToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
