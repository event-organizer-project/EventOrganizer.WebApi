using AutoFixture;
using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Queries.EventQueries;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;
using Moq;

namespace EventOrganizer.Test.Core.Queries.EventQueries
{
    public class GetEventListQueryTest : BaseTest<GetEventListQuery>
    {
        private Mock<IEventRepository> eventRepositoryMock;

        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            eventRepositoryMock = new Mock<IEventRepository>();
            mapperMock = new Mock<IMapper>();

            underTest = new GetEventListQuery(eventRepositoryMock.Object, mapperMock.Object);
        }

        [Test]
        public void Execute_Should_Return_Expected_Result()
        {
            var parameters = new GetEventListQueryParameters
            {
                Top = 2,
                Skip = 1
            };

            var eventModels = fixture.CreateMany<EventModel>(4).ToArray();

            var expectedResult = fixture.CreateMany<EventDTO>(2).ToList();

            eventRepositoryMock.Setup(x => x.GetAll())
                .Returns(eventModels);

            mapperMock.Setup(x => x.Map<EventDTO>(eventModels[1]))
                .Returns(expectedResult[0]);
            mapperMock.Setup(x => x.Map<EventDTO>(eventModels[2]))
                .Returns(expectedResult[1]);

            var result = underTest.Execute(parameters);

            Assert.That(result, Has.Count.EqualTo(expectedResult.Count));

            for(int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i], Is.EqualTo(expectedResult[i]));
            }
        }
    }
}
