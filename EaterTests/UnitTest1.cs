using MobileEater.ViewModels;
using NUnit.Framework;
using Moq;
using MobileEater.Services;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;

namespace EaterTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestSuccessfulRegistration()
        {
            var mock = new Mock<IGameService>();
            mock.Setup(m => m.JoinGame("player")).ReturnsAsync(new string("token"));

            var gvm = new GameViewModel(mock.Object);

            gvm.ServerAddress = "https://hungrygame.azurewebsites.net/";
            //gvm.PlayerName = "player";

            await gvm.JoinGameCommand.ExecuteAsync(this);

            gvm.Players.Any().Should().BeTrue();
        }
          
        [Test]
        public async Task TestFailedRegistrationWrongServerAddress()
        {
            var mock = new Mock<IGameService>();
            mock.Setup(m => m.JoinGame("player")).ReturnsAsync(new string("token"));

            var gvm = new GameViewModel(mock.Object);

            gvm.ServerAddress = "https://hungry.azurewebsites.net/";
            //gvm.PlayerName = "player";

            await gvm.JoinGameCommand.ExecuteAsync(this);

            gvm.Players.Any().Should().BeFalse();
        }
    }
}