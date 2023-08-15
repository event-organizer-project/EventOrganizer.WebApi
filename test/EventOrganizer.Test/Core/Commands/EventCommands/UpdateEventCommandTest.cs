using AutoFixture;
using AutoMapper;
using EventOrganizer.Core.Commands.EventCommands;
using EventOrganizer.Core.CustomExceptions;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;
using Moq;

namespace EventOrganizer.Test.Core.Commands.EventCommands
{
    internal class UpdateEventCommandTest : BaseTest<UpdateEventCommand>
    {
        private Mock<IEventRepository> eventRepositoryMock;
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            eventRepositoryMock = new Mock<IEventRepository>();
            mapperMock = new Mock<IMapper>();

            underTest = new UpdateEventCommand(eventRepositoryMock.Object, mapperMock.Object);
        }

        [Test]
        public void Execute_When_Model_Not_Found_Should_Throw_Exception()
        {
            var parameters = fixture.Create<UpdateEventCommandParameters>();

            Assert.Throws<ResourceNotFoundException>(() =>
                underTest.Execute(parameters));
        }

        [Test]
        public void Execute_Should_Return_Expected_Result()
        {
            var parameters = fixture.Create<UpdateEventCommandParameters>();

            var eventModel = fixture.Create<EventModel>();

            var expectedResult = fixture.Create<EventDetailDTO>();

            eventRepositoryMock.Setup(x => x.Get(parameters.EventDetailDTO.Id))
                .Returns(eventModel);

            mapperMock.Setup(x => x.Map(parameters.EventDetailDTO, eventModel))
                .Returns(eventModel);

            eventRepositoryMock.Setup(x => x.Update(eventModel))
                .Returns(eventModel);

            mapperMock.Setup(x => x.Map<EventDetailDTO>(eventModel))
                .Returns(expectedResult);
            
            var result = underTest.Execute(parameters);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
