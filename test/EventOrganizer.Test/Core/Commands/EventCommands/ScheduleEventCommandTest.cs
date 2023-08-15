using AutoFixture;
using AutoMapper;
using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.CustomExceptions;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;
using Moq;

namespace EventOrganizer.Test.Core.Commands.EventCommands
{
    public class ScheduleEventCommandTest : BaseTest<ScheduleEventCommand>
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

            underTest = new ScheduleEventCommand(eventRepositoryMock.Object, mapperMock.Object, userHandlerMock.Object);
        }

        [Test]
        public void Execute_When_Model_Not_Found_Should_Throw_Exception()
        {
            var parameters = fixture.Create<ScheduleEventCommandParameters>();

            Assert.Throws<ResourceNotFoundException>(() =>
                underTest.Execute(parameters));
        }

        [Test]
        public void Execute_When_Schedule_Event_Should_Return_Expected_Result()
        {
            var eventModel = fixture.Create<EventModel>();

            var parameters = fixture.Build<ScheduleEventCommandParameters>()
                .With(x => x.EventId, eventModel.Id)
                .With(x => x.IsEventScheduled, true)
                .Create();

            var currentUser = fixture.Create<User>();

            var expectedResult = fixture.Create<EventDetailDTO>();

            eventRepositoryMock.Setup(x => x.Get(parameters.EventId))
                .Returns(eventModel);

            userHandlerMock.Setup(x => x.GetCurrentUser())
                .Returns(currentUser);

            eventRepositoryMock.Setup(x => x.Update(eventModel))
                .Returns(eventModel);

            mapperMock.Setup(x => x.Map<EventDetailDTO>(eventModel))
                .Returns(expectedResult);

            var actualResult = underTest.Execute(parameters);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Execute_When_Unschedule_Event_Should_Return_Expected_Result()
        {
            var currentUser = fixture.Create<User>();

            var eventModel = fixture.Build<EventModel>()
                .With(x => x.Members, new List<User> { currentUser  })
                .Create();

            var parameters = fixture.Build<ScheduleEventCommandParameters>()
                .With(x => x.EventId, eventModel.Id)
                .With(x => x.IsEventScheduled, false)
                .Create();

            var expectedResult = fixture.Create<EventDetailDTO>();

            eventRepositoryMock.Setup(x => x.Get(parameters.EventId))
                .Returns(eventModel);

            userHandlerMock.Setup(x => x.GetCurrentUser())
                .Returns(currentUser);

            eventRepositoryMock.Setup(x => x.Update(eventModel))
                .Returns(eventModel);

            mapperMock.Setup(x => x.Map<EventDetailDTO>(eventModel))
                .Returns(expectedResult);

            var actualResult = underTest.Execute(parameters);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
