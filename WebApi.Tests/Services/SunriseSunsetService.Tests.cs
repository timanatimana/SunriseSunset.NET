using FluentAssertions;
using NSubstitute;
using System.Net;
using System.Text;
using WebApi.Models;
using WebApi.Models.SunriseSunset;
using WebApi.Services;
using WebApi.Tests.Helper;
using Xunit;
using static WebApi.Utils.CommonUtils;

namespace WebApi.Tests.Services
{
    public class SunriseSunsetServiceTests
    {
        private readonly ISunriseSunsetService _sut;
        private readonly IHttpClientFactory _httpFactory;

        public SunriseSunsetServiceTests()
        {
            _httpFactory = Substitute.For<IHttpClientFactory>();

            _sut = new SunriseSunsetService(
                httpFactory: _httpFactory); ;
        }

        [Fact]
        public void GetSunsetSunriseAsync_returns_valid_response()
        {
            // Arrange
            var content = new StringContent(GetValidResultData(), Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = content
            };

            var fakeHttpMessageHandler = Substitute.ForPartsOf<FakeHttpMessageHandler>();
            var httpClient = new HttpClient(fakeHttpMessageHandler);
            fakeHttpMessageHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
                .Returns(response);
            _httpFactory.CreateClient().Returns(httpClient);

            // Act
            var result = _sut.GetSunsetSunriseAsync(GetValidRequestData());

            // Assert
            result.Should().NotBeNull();
            var data = result.Should().BeOfType<Task<SunriseSunset>>().Subject;
        }

        [Fact]
        public void GetSunsetSunriseAsync_returns_null()
        {
            // Arrange
            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = content
            };

            var fakeHttpMessageHandler = Substitute.ForPartsOf<FakeHttpMessageHandler>();
            var httpClient = new HttpClient(fakeHttpMessageHandler);
            fakeHttpMessageHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
                .Returns(response);
            _httpFactory.CreateClient().Returns(httpClient);

            // Act
            var result = _sut.GetSunsetSunriseAsync(GetValidRequestData());

            // Assert
            result.Should().BeOfType<Task<SunriseSunset>>();
            result.Result.Should().BeNull();
        }

        public static SunriseSunsetRequestData GetValidRequestData()
        {
            return new SunriseSunsetRequestData()
            {
                Coordinates = new Coordinates { Latitude = 46.704703, Longitude = 14.730967 },
                DateString = "today",
                Timezone = Timezone.UTC
            };
        }

        public static string GetValidResultData()
        {
            var data = "{\r\n    \"results\": {\r\n        \"sunrise\": \"4:05:01 AM\",\r\n        \"sunset\": \"5:56:57 PM\",\r\n        \"first_light\": \"2:09:30 AM\",\r\n        \"last_light\": \"7:52:28 PM\",\r\n        \"dawn\": \"3:32:41 AM\",\r\n        \"dusk\": \"6:29:17 PM\",\r\n        \"solar_noon\": \"11:00:59 AM\",\r\n        \"golden_hour\": \"5:15:46 PM\",\r\n        \"day_length\": \"13:51:56\",\r\n        \"timezone\": \"UTC\"\r\n    },\r\n    \"status\": \"OK\"\r\n}";
            return data;
        }
    }
}
