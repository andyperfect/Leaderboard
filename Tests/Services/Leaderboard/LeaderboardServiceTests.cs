using System;
using Moq;
using NUnit.Framework;
using Services.Leaderboard;
using Services.Leaderboard.Models;

namespace Tests.Services.Leaderboard
{
    [TestFixture]
    public class LeaderboardServiceTests
    {
        private Mock<ILeaderboardRepository> _fakeLeaderboardRepository;
        private LeaderboardService _sut;
        
        [SetUp]
        public void SetUp()
        {
            _fakeLeaderboardRepository = new Mock<ILeaderboardRepository>();
            _sut = new LeaderboardService(_fakeLeaderboardRepository.Object);
        }

        [Test]
        public void UpdateTitle_UnsuccessfulUpdate_ThrowsError()
        {
            const long id = 70;
            const string newTitle = "newTitle";
            _fakeLeaderboardRepository.Setup(x => x.UpdateTitle(id, newTitle)).Returns(false);
            Assert.Throws<Exception>(() => _sut.UpdateTitle(id, newTitle));
        }

        [Test]
        public void UpdateTitle_SuccessfulUpdate_ReturnsNewModel()
        {
            const long id = 75;
            const string newTitle = "Earthbound";
            var expectedLeaderboardResult = new LeaderboardModel
            {
                Id = id,
                Title = newTitle,
                AddedBy = 2,
                DateCreated = 19
            };
            _fakeLeaderboardRepository.Setup(x => x.UpdateTitle(id, newTitle)).Returns(true);
            _fakeLeaderboardRepository.Setup(x => x.Get(id)).Returns(expectedLeaderboardResult);
            var actual = _sut.UpdateTitle(id, newTitle);
            Assert.AreEqual(expectedLeaderboardResult, actual);
        }
    }
}