using AutoFixture;
using EventOrganizer.Core.CustomExceptions;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;
using Moq;

namespace EventOrganizer.Test.Core.Services
{
    public class UserHandlerTest : BaseTest<UserHandler>
    {
        private Mock<IUserContextAccessor> userContextAccessorMock;
        private Mock<IUserRepository> userRepositoryMock;

        [SetUp]
        public void Setup()
        {
            userContextAccessorMock = new Mock<IUserContextAccessor>();
            userRepositoryMock = new Mock<IUserRepository>();

            underTest = new UserHandler(userContextAccessorMock.Object, userRepositoryMock.Object);
        }

        [Test]
        public void GetCurrentUser_Should_Return_Expected_Result()
        {
            var userId = 1;

            var expectedResult = fixture.Create<User>();

            userContextAccessorMock.Setup(x => x.GetUserId())
                .Returns(userId);

            userRepositoryMock.Setup(x => x.GetUserById(userId))
                .Returns(expectedResult);

            var actualResult = underTest.GetCurrentUser();

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetCurrentUser_When_User_Not_Found_Should_Throw_UserHandlingException()
        {
            var userId = 1;

            var expectedResult = fixture.Create<User>();

            userContextAccessorMock.Setup(x => x.GetUserId())
                .Returns(userId);

            Assert.Throws<UserHandlingException>(() =>
                underTest.GetCurrentUser());
        }
    }
}
