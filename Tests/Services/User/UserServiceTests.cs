using System;
using Moq;
using NUnit.Framework;
using Services.Password;
using Services.User;
using Services.User.Models;

namespace Tests.Services.User
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _fakeUserRepository;
        private UserService _sut;

        [SetUp]
        public void SetUp()
        {
            _fakeUserRepository = new Mock<IUserRepository>();
            _sut = new UserService(_fakeUserRepository.Object);
        }

        [Test]
        public void GetAccessToken_UserDoesNotExist_ThrowsException()
        {
            const string username = "username1";
            _fakeUserRepository.Setup(x => x.GetFullPasswordUser(username)).Returns((UserPasswordModel) null);
            Assert.Throws<Exception>(() => _sut.GetAccessToken(username, ""));
        }

        [Test]
        public void GetAccessToken_InvalidPassword_ThrowsException()
        {
            const string username = "username";
            const string password = "password";
            const string salt = "salt";
            var hashedPassword = PasswordService.HashPassword(password, salt);
            _fakeUserRepository.Setup(x => x.GetFullPasswordUser(username)).Returns(new UserPasswordModel()
            {
                HashedPassword = hashedPassword,
                Salt = salt,
                Username = username
            });
            Assert.Throws<Exception>(() => _sut.GetAccessToken(username, ""));
        }

        [Test]
        public void GetAccessToken_ValidPassword_ReturnsAccessToken()
        {
            const string username = "username";
            const string password = "password";
            const string salt = "salt";
            const string accessToken = "myAccessToken";
            var hashedPassword = PasswordService.HashPassword(password, salt);
            _fakeUserRepository.Setup(x => x.GetFullPasswordUser(username)).Returns(new UserPasswordModel()
            {
                AccessToken = accessToken,
                HashedPassword = hashedPassword,
                Salt = salt,
                Username = username
            });
            var actual = _sut.GetAccessToken(username, password);
            Assert.That(accessToken, Is.EqualTo(actual));
        }
    }
}
