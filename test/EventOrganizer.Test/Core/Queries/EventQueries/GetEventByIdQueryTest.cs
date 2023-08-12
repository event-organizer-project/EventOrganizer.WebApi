using AutoFixture;
using AutoMapper;
using EventOrganizer.Core.CustomExceptions;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Queries.EventQueries;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;
using Moq;

namespace EventOrganizer.Test.Core.Queries.EventQueries
{
    public class GetEventByIdQueryTest : BaseTest<GetEventByIdQuery>
    {
        private Mock<IEventRepository> eventRepositoryMock;

        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            eventRepositoryMock = new Mock<IEventRepository>();
            mapperMock = new Mock<IMapper>();

            underTest = new GetEventByIdQuery(eventRepositoryMock.Object, mapperMock.Object);
        }

        [Test]
        public void Execute_Should_Return_Expected_Result()
        {
            var parameters = fixture.Create<GetEventByIdQueryParameters>();

            var eventModel = fixture.Create<EventModel>();

            var expectedResult = fixture.Create<EventDetailDTO>();

            eventRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(eventModel);

            mapperMock.Setup(x => x.Map<EventDetailDTO>(eventModel))
                .Returns(expectedResult);

            var result = underTest.Execute(parameters);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Execute_Should_Throw_ResourceNotFoundException()
        {
            eventRepositoryMock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(null as EventModel);

            Assert.Throws<ResourceNotFoundException>(() =>
                underTest.Execute(new GetEventByIdQueryParameters { Id = 1 }));
        }
    }
}
