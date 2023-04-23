using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Net;
using System.Text;
using WebApi.Config;
using WebApi.Models.Positionstack;
using WebApi.Services;
using WebApi.Tests.Helper;
using Xunit;

namespace WebApi.Tests.Services
{
    public class PositionstackServiceTests
    {
        private readonly IPositionstackService _sut;
        private readonly IHttpClientFactory _httpFactory;
        private IOptions<Positionstack> _positionstackConfig;

        public PositionstackServiceTests()
        {
            _httpFactory = Substitute.For<IHttpClientFactory>();
            _positionstackConfig = Substitute.For<IOptions<Positionstack>>();

            var Positionstack = new Positionstack() { ApiKey = "123" };
            _positionstackConfig.Value.Returns(Positionstack);

            _sut = new PositionstackService(
                httpFactory: _httpFactory,
                opts: _positionstackConfig); ;
        }

        [Fact]
        public void GetLocationDataAsync_returns_valid_response()
        {
            // Arrange
            var location = "Griffen";

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
            var result = _sut.GetLocationDataAsync(location);

            // Assert
            result.Should().NotBeNull();
            var data = result.Should().BeOfType<Task<List<LocationData>>>().Subject;
            data.Result.Count.Should().Be(4);
            data.Result[0].Id.Should().Be(1);
        }

        [Fact]
        public void GetLocationDataAsync_returns_empty_array()
        {
            // Arrange
            var location = "lsdfjkfsd";

            var content = new StringContent("{\"data\":[]}", Encoding.UTF8, "application/json");
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
            var result = _sut.GetLocationDataAsync(location);

            // Assert
            result.Should().NotBeNull();
            var data = result.Should().BeOfType<Task<List<LocationData>>>().Subject;
            data.Result.Count.Should().Be(0);
        }

        [Fact]
        public void GetLocationDataAsync_returns_null()
        {
            // Arrange
            var location = "";

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
            var result = _sut.GetLocationDataAsync(location);

            // Assert
            result.Should().BeOfType<Task<List<LocationData>>>();
            result.Result.Should().BeNull();
        }

        public static string GetValidResultData()
        {
            var data = "{\"data\":[{\"latitude\":46.704703,\"longitude\":14.730967,\"type\":\"locality\",\"name\":\"Griffen\",\"number\":null,\"postal_code\":null,\"street\":null,\"confidence\":1,\"region\":\"Carinthia\",\"region_code\":\"KA\",\"county\":\"V\\u00f6lkermarkt\",\"locality\":\"Griffen\",\"administrative_area\":\"Griffen\",\"neighbourhood\":null,\"country\":\"Austria\",\"country_code\":\"AUT\",\"continent\":\"Europe\",\"label\":\"Griffen, KA, Austria\",\"timezone_module\":{\"name\":\"Europe\\/Vienna\",\"offset_sec\":7200,\"offset_string\":\"+02:00\"}},{\"latitude\":42.18776,\"longitude\":-93.79495,\"type\":\"locality\",\"name\":\"Griffen\",\"number\":null,\"postal_code\":null,\"street\":null,\"confidence\":1,\"region\":\"Iowa\",\"region_code\":\"IA\",\"county\":\"Boone County\",\"locality\":\"Griffen\",\"administrative_area\":null,\"neighbourhood\":null,\"country\":\"United States\",\"country_code\":\"USA\",\"continent\":\"North America\",\"label\":\"Griffen, IA, USA\",\"timezone_module\":{\"name\":\"America\\/Chicago\",\"offset_sec\":-18000,\"offset_string\":\"-05:00\"}},{\"latitude\":35.2762,\"longitude\":-89.50035,\"type\":\"locality\",\"name\":\"Griffen\",\"number\":null,\"postal_code\":null,\"street\":null,\"confidence\":1,\"region\":\"Tennessee\",\"region_code\":\"TN\",\"county\":\"Fayette County\",\"locality\":\"Griffen\",\"administrative_area\":null,\"neighbourhood\":null,\"country\":\"United States\",\"country_code\":\"USA\",\"continent\":\"North America\",\"label\":\"Griffen, TN, USA\",\"timezone_module\":{\"name\":\"America\\/Indiana\\/Tell_City\",\"offset_sec\":-18000,\"offset_string\":\"-05:00\"}},{\"latitude\":32.03822,\"longitude\":-95.0755,\"type\":\"locality\",\"name\":\"Griffin\",\"number\":null,\"postal_code\":null,\"street\":null,\"confidence\":1,\"region\":\"Texas\",\"region_code\":\"TX\",\"county\":\"Cherokee County\",\"locality\":\"Griffin\",\"administrative_area\":null,\"neighbourhood\":null,\"country\":\"United States\",\"country_code\":\"USA\",\"continent\":\"North America\",\"label\":\"Griffin, TX, USA\",\"timezone_module\":{\"name\":\"America\\/Indiana\\/Tell_City\",\"offset_sec\":-18000,\"offset_string\":\"-05:00\"}}]}";
            return data;
        }
    }
}
