using FluentAssertions;
using WebApi.Models;
using WebApi.Utils;
using Xunit;
using static WebApi.Utils.CommonUtils;

namespace WebApi.Tests.Utils
{
    public class CommonUtilsTests
    {
        public static IEnumerable<object[]> GetTestValuesSunriseSunsetWithTimezone()
        {
            yield return new object[]
            {
                new Coordinates { Latitude = 46.704703, Longitude = 14.730967 },
                "today",
                Timezone.CEST
            };
        }

        public static IEnumerable<object[]> GetTestValuesSunriseSunsetWithoutTimezone()
        {
            yield return new object[]
            {
                new Coordinates { Latitude = 46.704703, Longitude = 14.730967 },
                "tomorrow"
            };
        }


        [Theory]
        [MemberData(nameof(GetTestValuesSunriseSunsetWithTimezone))]
        public void BuildSunriseSunsetUrl_returns_valid_Url(Coordinates coordinates, string date, Timezone timezone)
        {
            // Arrange

            // Act
            var result = CommonUtils.BuildSunriseSunsetUrl(coordinates, date, (Timezone)timezone);

            // Assert
            result.Should().Be($"https://api.sunrisesunset.io/json?lat=46.704703&lng=14.730967&timezone=CEST&date={date}");
        }

        [Theory]
        [MemberData(nameof(GetTestValuesSunriseSunsetWithoutTimezone))]
        public void BuildSunriseSunsetUrl_without_timezone_parameter_returns_valid_Url(Coordinates coordinates, string date)
        {
            // Arrange

            // Act
            var result = CommonUtils.BuildSunriseSunsetUrl(coordinates, date);

            // Assert
            result.Should().Be($"https://api.sunrisesunset.io/json?lat=46.704703&lng=14.730967&timezone=UTC&date={date}");
        }

        [Fact]
        public void BuildPositionstackUrl_returns_valid_Url()
        {
            // Arrange
            var accessKey = "12345";
            var location = "Testlocation";

            // Act
            var result = CommonUtils.BuildPositionstackUrl(accessKey, location);

            // Assert
            result.Should().Be("http://api.positionstack.com/v1/forward?access_key=12345&query=Testlocation&timezone_module=1");
        }
    }
}
