using AutoFixture;
using AutoMapper;
using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Queries.EventQueries;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;
using Moq;

namespace EventOrganizer.Test.Core.Queries.EventQueries
{
    public class GetCurrentUserOwnEventsQueryTest : BaseTest<GetCurrentUserOwnEventsQuery>
    {
        private Mock<IEventRepository> eventRepositoryMock;
        private Mock<IMapper> mapperMock;
        private Mock<IUserHandler> userHandlerMock;

        [SetUp]
        public void Setup()
        {
            eventRepositoryMock = new Mock<IEventRepository>();
            mapperMock = new Mock<IMapper>();
            userHandlerMock = new Mock<IUserHandler>();

            underTest = new GetCurrentUserOwnEventsQuery(
                eventRepositoryMock.Object,
                mapperMock.Object,
                userHandlerMock.Object);
        }

        [Test]
        public void Execute_Should_Return_Expected_Result()
        {
            var currentUser = fixture.Create<User>();

            var eventModels = fixture.CreateMany<EventModel>(3).ToArray();

            eventModels[0].Owner = currentUser;
            eventModels[1].Members.Add(currentUser);

            var createdEvent = fixture.Create<EventDTO>();
            var joinedEvent = fixture.Create<EventDTO>();

            mapperMock.Setup(x => x.Map<EventDTO>(eventModels[0]))
                .Returns(createdEvent);
            mapperMock.Setup(x => x.Map<EventDTO>(eventModels[1]))
                .Returns(joinedEvent);

            userHandlerMock.Setup(x => x.GetCurrentUser())
                .Returns(currentUser);

            eventRepositoryMock.Setup(x => x.GetAll())
                .Returns(eventModels);

            var actualResult = underTest.Execute(new VoidParameters());

            Assert.That(actualResult.CreatedEvents, Does.Contain(createdEvent));
            Assert.That(actualResult.JoinedEvents, Does.Contain(joinedEvent));
        }
    }
}
