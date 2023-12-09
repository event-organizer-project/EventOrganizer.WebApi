using AutoFixture;
using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;
using Moq;

namespace EventOrganizer.Test.Core.Commands.EventCommands
{
    public class DeleteEventCommandTest : BaseTest<DeleteEventCommand>
    {
        private Mock<IEventRepository> eventRepositoryMock;
        private Mock<ISchedulerClient> schedulerClient;

        [SetUp]
        public void Setup()
        {
            eventRepositoryMock = new Mock<IEventRepository>();
            schedulerClient = new Mock<ISchedulerClient>();

            underTest = new DeleteEventCommand(eventRepositoryMock.Object, schedulerClient.Object);
        }

        [Test]
        public void Execute_Should_Return_Expected_Result()
        {
            var parameters = fixture.Create<DeleteEventCommandParameters>();

            var deletedEvent = fixture.Build<EventModel>()
                .With(x => x.StartDate, DateTime.Today)
                .Create();

            var expectedResult = fixture.Create<EventDetailDTO>();

            eventRepositoryMock.Setup(x => x.Delete(parameters.EventId))
                .Returns(deletedEvent);

            var actualResult = underTest.Execute(parameters);

            eventRepositoryMock.Verify(x => x.Delete(parameters.EventId), Times.Once);
            schedulerClient.Verify(x => x.RemoveEventFromSchedule(deletedEvent.Id, null), Times.Once);
        }
    }
}
