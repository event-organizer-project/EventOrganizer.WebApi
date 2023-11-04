using AutoFixture;
using AutoMapper;
using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;
using Moq;

namespace EventOrganizer.Test.Core.Commands.EventCommands
{
    public class CreateEventCommandTest : BaseTest<CreateEventCommand>
    {
        private Mock<IEventRepository> eventRepositoryMock;
        private Mock<IMapper> mapperMock;
        private Mock<IUserHandler> userHandlerMock;
        private Mock<ISchedulerClient> schedulerClient;

        [SetUp]
        public void Setup()
        {
            eventRepositoryMock = new Mock<IEventRepository>();
            mapperMock = new Mock<IMapper>();
            userHandlerMock = new Mock<IUserHandler>();
            schedulerClient = new Mock<ISchedulerClient>();

            underTest = new CreateEventCommand(eventRepositoryMock.Object,
                mapperMock.Object, userHandlerMock.Object, schedulerClient.Object);
        }

        [Test]
        public void Execute_Should_Return_Expected_Result()
        {
            var parameters = fixture.Create<CreateEventCommandParameters>();

            var currentUser = fixture.Create<User>();

            var eventModel = fixture.Create<EventModel>();

            var expectedResult = fixture.Create<EventDetailDTO>();

            mapperMock.Setup(x => x.Map<EventModel>(parameters.EventDetailDTO))
                .Returns(eventModel);

            userHandlerMock.Setup(x => x.GetCurrentUser())
                .Returns(currentUser);

            eventRepositoryMock.Setup(x => x.Create(eventModel))
                .Returns(eventModel);

            mapperMock.Setup(x => x.Map<EventDetailDTO>(eventModel))
                .Returns(expectedResult);

            var actualResult =  underTest.Execute(parameters);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
