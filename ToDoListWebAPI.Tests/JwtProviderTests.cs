using BusinessLogic.LogicServices.Interfaces.Auth;
using BusinessLogic.LogicServices.Services.Auth;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using Xunit;

namespace ToDoListWebAPI.Tests
{
    /// <summary>
    /// Unit-тесты JwtProvider.
    /// </summary>
    public class JwtProviderTests
    {
        private static JwtProvider CreateProvider()
        {
            var options = Options.Create(new JwtOptions
            {
                SecretKey = "test-secret-key-0123456789-0123456789-0123456789",
                Issuer = "ToDoListWebAPI",
                Audience = "ToDoListClient",
                ExpiresHours = 12
            });

            return new JwtProvider(options);
        }

        [Fact]
        public void GenerateToken_ReturnsValidJwtWithUserIdClaim()
        {
            var provider = CreateProvider();
            var user = new ApplicationUser { Id = Guid.NewGuid(), Email = "test@example.com" };

            var token = provider.GenerateToken(user);

            var handler = new JwtSecurityTokenHandler();
            Assert.True(handler.CanReadToken(token));

            var jwt = handler.ReadJwtToken(token);
            var userIdClaim = jwt.Claims.FirstOrDefault(c => c.Type == "userId");

            Assert.NotNull(userIdClaim);
            Assert.Equal(user.Id.ToString(), userIdClaim!.Value);
        }

        [Fact]
        public void GenerateToken_SetsIssuerAudienceAndExpiry()
        {
            var provider = CreateProvider();
            var user = new ApplicationUser { Id = Guid.NewGuid() };

            var token = provider.GenerateToken(user);

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            Assert.Equal("ToDoListWebAPI", jwt.Issuer);
            Assert.Contains("ToDoListClient", jwt.Audiences);
            Assert.True(jwt.ValidTo > DateTime.UtcNow.AddHours(11));
            Assert.True(jwt.ValidTo <= DateTime.UtcNow.AddHours(12).AddMinutes(1));
        }
    }

    /// <summary>
    /// Unit-тесты JwtProvider с Moq: проверка взаимодействия с IOptions.
    /// </summary>
    public class JwtProviderMockTests
    {
        [Fact]
        public void GenerateToken_UsesConfiguredOptions()
        {
            var optionsMock = new Mock<IOptions<JwtOptions>>();
            optionsMock
                .Setup(o => o.Value)
                .Returns(new JwtOptions
                {
                    SecretKey = "another-secret-key-0123456789-0123456789abcdef",
                    Issuer = "TestIssuer",
                    Audience = "TestAudience",
                    ExpiresHours = 1
                });

            var provider = new JwtProvider(optionsMock.Object);
            var user = new ApplicationUser { Id = Guid.NewGuid() };

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(provider.GenerateToken(user));

            Assert.Equal("TestIssuer", jwt.Issuer);
            Assert.Contains("TestAudience", jwt.Audiences);
        }
    }
}
