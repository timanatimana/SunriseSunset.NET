using System.ComponentModel;
using System.Globalization;
using WebApi.Models;

namespace WebApi.Utils
{
    public class CommonUtils
    {
        public enum Timezone
        {
            [Description("Coordinated Universal Time")]
            UTC = 1,
            [Description("Central European Summer Time")]
            CEST = 2, 
            [Description("Central European Time")]
            CET = 3,

        }

        // could also be used instead of BuildSunriseSunsetUrl date
        public const string Date_Today = "today";
        public const string Date_Tomorrow = "tomorrow";

        public static string BuildSunriseSunsetUrl(Coordinates coordinates, string date, Timezone timezone = Timezone.UTC)
        {
            return $"https://api.sunrisesunset.io/json" +
                   $"?lat={coordinates.Latitude.ToString(new CultureInfo("en-US"))}" +
                   $"&lng={coordinates.Longitude.ToString(new CultureInfo("en-US"))}" +
                   $"&timezone={timezone}" +
                   $"&date={date}";
        }

        public static string BuildPositionstackUrl(string accessKey, string location)
        {
            return $"http://api.positionstack.com/v1/forward" +
                   $"?access_key={accessKey}" +
                   $"&query={location}" +
                   $"&timezone_module=1";
        }

    }
}
