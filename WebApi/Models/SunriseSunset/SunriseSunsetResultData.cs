using System.Text.Json.Serialization;

namespace WebApi.Models.SunriseSunset
{
    public class SunriseSunsetResultData
    {
        [JsonPropertyName("sunrise")]
        public string Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public string Sunset { get; set; }

        [JsonPropertyName("first_light")]
        public string FirstLight { get; set; }

        [JsonPropertyName("last_light")]
        public string LastLight { get; set; }

        [JsonPropertyName("dawn")]
        public string Dawn { get; set; }

        [JsonPropertyName("dusk")]
        public string Dusk { get; set; }

        [JsonPropertyName("solar_noon")]
        public string SolarNoon { get; set; }

        [JsonPropertyName("golden_hour")]
        public string GoldenHour { get; set; }

        [JsonPropertyName("day_length")]
        public string DayLength { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
    }
}
