using EventOrganizer.WebApi.Services;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security;
using System.Security.Claims;

namespace EventOrganizer.Test.WebApi.Services
{
    public class UserContextAccessorTest : BaseTest<UserContextAccessor>
    {
        private Mock<IHttpContextAccessor> httpContextAccessorMock;
        private Mock<HttpContext> httpContextMock;
        private Mock<ClaimsPrincipal> userMock;

        [SetUp]
        public void Setup()
        {
            var claims = new Claim[] { new Claim(JwtClaimTypes.Id, "1") };

            userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(x => x.Claims).Returns(claims);

            httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.User).Returns(userMock.Object);

            httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext)
                .Returns(httpContextMock.Object);

            underTest = new UserContextAccessor(httpContextAccessorMock.Object);
        }

        [Test]
        public void GetUserContext_Should_Return_Expected_Result()
        {
            var result = underTest.GetUserContext();

            Assert.That(result, Is.EqualTo(userMock.Object));
        }

        [Test]
        public void GetUserContext_Should_Throw_SecurityException()
        {
            httpContextAccessorMock.Setup(x => x.HttpContext)
                .Returns((HttpContext)null);

            var exception = Assert.Throws<SecurityException>(() =>
                underTest.GetUserContext());

            Assert.That(exception.Message, Is.EqualTo("HttpContext does not exist"));
        }

        [Test]
        public void GetUserId_Should_Return_Expected_Result()
        {
            const int userId = 1;

            var result = underTest.GetUserId();

            Assert.That(result, Is.EqualTo(userId));
        }

        [Test]
        public void GetUserId_Without_Required_Claim_Should_Throw_SecurityException()
        {
            var claims = Array.Empty<Claim>();

            userMock.Setup(x => x.Claims).Returns(claims);

            var exception = Assert.Throws<SecurityException>(() =>
                underTest.GetUserId());

            Assert.That(exception.Message, Is.EqualTo("Claim does not exist"));
        }

        [Test]
        public void GetUserId_With_Invalid_Claim_Should_Throw_SecurityException()
        {
            var claims = new Claim[] { new Claim(JwtClaimTypes.Id, "invalid_claim") };

            userMock.Setup(x => x.Claims).Returns(claims);

            var exception = Assert.Throws<SecurityException>(() =>
                underTest.GetUserId());

            Assert.That(exception.Message, Is.EqualTo("Incorrect user id claim format"));
        }
    }
}
