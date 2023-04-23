using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Net;
using System.Text;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Models.Positionstack;
using WebApi.Models.SunriseSunset;
using WebApi.Services;
using WebApi.Tests.Helper;
using Xunit;

namespace WebApi.Tests.Controllers
{
    public class PositionstackControllerTests
    {
        private readonly PositionstackController _sut;
        private readonly ILogger<PositionstackController> _logger;
        private readonly IPositionstackService _positionstackService;

        public PositionstackControllerTests()
        {
            _logger = Substitute.For<ILogger<PositionstackController>>();
            _positionstackService = Substitute.For<IPositionstackService>();

            _sut = new PositionstackController(
                logger: _logger, 
                positionstackService: _positionstackService);
        }

        [Fact]
        public void GetLocationData_valid_result_returns_Ok()
        {
            // Arrange
            var location = "Testlocation";

            _positionstackService.GetLocationDataAsync(default!).ReturnsForAnyArgs(GetValidData());

            // Act
            var result = _sut.GetLocationData(location);

            // Assert
            result.Should().NotBeNull();
            var data = result.Should().BeOfType<Task<IActionResult>>().Subject;
            data.Result.Should().NotBeNull();
            var response = data.Result.Should().BeOfType<OkObjectResult>().Subject;
            response.Value.Should().NotBeNull();
            var resData = response.Value.Should().BeOfType<List<LocationData>>().Subject;
            resData.Count.Should().Be(1);
        }

        [Fact]
        public void GetLocationData_empty_result_returns_NoContent()
        {
            // Arrange
            var location = "Testlocation";

            _positionstackService.GetLocationDataAsync(default!).ReturnsForAnyArgs(new List<LocationData>());

            // Act
            var result = _sut.GetLocationData(location);

            // Assert
            result.Should().NotBeNull();
            var data = result.Should().BeOfType<Task<IActionResult>>().Subject;
            data.Result.Should().NotBeNull();
            var response = data.Result.Should().BeOfType<NoContentResult>().Subject;
        }

        [Fact]
        public void GetLocationData_null_result_returns_NotFound()
        {
            // Arrange
            var location = "Testlocation";

            _positionstackService.GetLocationDataAsync(default!).ReturnsNullForAnyArgs();

            // Act
            var result = _sut.GetLocationData(location);

            // Assert
            result.Should().NotBeNull();
            var data = result.Should().BeOfType<Task<IActionResult>>().Subject;
            data.Result.Should().NotBeNull();
            data.Result.Should().BeOfType<NotFoundResult>();
        }

        public static List<LocationData> GetValidData()
        {
            return new List<LocationData>()
            {
                new()
                {
                    Id = 1,
                    Latitude = 0,
                    Longitude = 0,
                    Name = "",
                    Region = "",
                    Country = "",
                    Continent = "",
                    Label = "",
                    TimezoneString = ""
                }
            };
        }
    }
}
