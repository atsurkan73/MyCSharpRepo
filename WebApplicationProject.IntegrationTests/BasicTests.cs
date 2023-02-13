using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplicationProject.IntegrationTests
{
    public class BasicTests: IClassFixture<WebApplicationFactory<Program>>
    {
     private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("Home/Index")]
        [InlineData("Home/Login")]
        [InlineData("Home/TariffPlans")]
        [InlineData("Home/ServiceProfiles")]
        [InlineData("Home/EditCustomer")]
        [InlineData("Home/EditServiceProfile")]
        [InlineData("Home/EditTariff")]
        [InlineData("Home/InputForm")]
        [InlineData("Home/InputFormProfile")]
        [InlineData("Home/InputFormTariff")]
        [InlineData("Home/Privacy")]

        public async Task GetPages(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            //Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299 
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }

        [Fact]
        public async Task GetRedirectIfNotAuth()
        {
            // Arrange
            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
            // Act
            var response = await client.GetAsync("/Home/EditCustomer/8001");
            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("/Home/Login",
                response.Headers.Location?.OriginalString);
        }

        [Fact]
        public async Task Get_SecurePageIsReturnedForAnAuthenticatedUser()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                    services.AddAuthentication(defaultScheme: "TestScheme")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "TestScheme", _ => { }));
            })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(scheme: "TestScheme");
            //Act
            var response = await client.GetAsync("/Home/EditCustomer/8001");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetRedirectIfNotAuth_ServiceProfiles()
        {
            // Arrange
            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
            // Act
            var response = await client.GetAsync("/Home/EditServiceProfile/100800");
            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("/Home/Login",
                response.Headers.Location?.OriginalString);
        }

        [Fact]
        public async Task Get_SecurePageIsReturnedForAnAuthenticatedUser_ServiceProfiles()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                    services.AddAuthentication(defaultScheme: "TestScheme")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "TestScheme", _ => { }));
            })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(scheme: "TestScheme");
            //Act
            var response = await client.GetAsync("/Home/EditServiceProfile/1008001");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetRedirectIfNotAuth_TariffPlans()
        {
            // Arrange
            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
            // Act
            var response = await client.GetAsync("/Home/EditTariff/50");
            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("/Home/Login",
                response.Headers.Location?.OriginalString);
        }

        [Fact]
        public async Task Get_SecurePageIsReturnedForAnAuthenticatedUser_TariffPlans()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                    services.AddAuthentication(defaultScheme: "TestScheme")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "TestScheme", _ => { }));
            })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(scheme: "TestScheme");
            //Act
            var response = await client.GetAsync("/Home/EditTariff/50");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
        {
            public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
                : base(options, logger, encoder, clock)
            {
            }

            protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, "Test admin"),
            new(ClaimsIdentity.DefaultRoleClaimType, "admin")
        };
                var identity = new ClaimsIdentity(claims, "Test");
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, "TestScheme");
                var result = AuthenticateResult.Success(ticket);
                return Task.FromResult(result);
            }
        }
}
}