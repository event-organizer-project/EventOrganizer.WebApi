using AutoFixture;
using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Queries.UserQueries;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;
using Moq;

namespace EventOrganizer.Test.Core.Queries.UserQueries
{
    public class GetCurrentUserQueryTest : BaseTest<GetCurrentUserQuery>
    {
        private Mock<IMapper> mapperMock;
        private Mock<IUserHandler> userHandlerMock;

        [SetUp]
        public void Setup()
        {
            mapperMock = new Mock<IMapper>();
            userHandlerMock = new Mock<IUserHandler>();

            underTest = new GetCurrentUserQuery(userHandlerMock.Object, mapperMock.Object);
        }

        [Test]
        public void Execute_Should_Return_Expected_Result()
        {
            var currentUser = fixture.Create<User>();

            var expectedResult = fixture.Create<UserDTO>();

            userHandlerMock.Setup(x => x.GetCurrentUser())
                .Returns(currentUser);

            mapperMock.Setup(x => x.Map<UserDTO>(currentUser))
                .Returns(expectedResult);

            var actualResult = underTest.Execute(new VoidParameters());

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
