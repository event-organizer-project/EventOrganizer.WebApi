using AutoFixture;
using EventOrganizer.Domain.Models;
using EventOrganizer.EF;
using EventOrganizer.EF.MySql.Repositories;
using Moq;

namespace EventOrganizer.Test.EF.MySql.Repositories
{
    public class UserRepositoryTest
    {
        private UserRepository underTest;

        private Mock<EventOrganazerDbContext> eventOrganazerDbContextMock;

        private Fixture fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new CustomFixture();

            eventOrganazerDbContextMock = new Mock<EventOrganazerDbContext>();

            underTest = new UserRepository(eventOrganazerDbContextMock.Object);
        }
        /*
        [Test]
        public void GetAll_Should_Return_Expected_Result()
        {
            var expectedResult = fixture.CreateMany<User>().ToList();

            eventOrganazerDbContextMock.Setup(x => x.Users)
                .Returns(DbSetMockFactory.Create(expectedResult).Object);

            var actualResult = underTest.GetAll().ToList();

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetUserById_Should_Return_Expected_Result()
        {
            var users = fixture.CreateMany<User>().ToList();

            eventOrganazerDbContextMock.Setup(x => x.Users)
                .Returns(DbSetMockFactory.Create(users).Object);

            var actualResult = underTest.GetUserById(users[0].Id);

            Assert.That(actualResult, Is.EqualTo(users[0]));
        }
        */
    }
}
