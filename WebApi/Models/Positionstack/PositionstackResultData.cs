using System.Text.Json.Serialization;

namespace WebApi.Models.Positionstack
{
    public class PositionstackResultData
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("continent")]
        public string Continent { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("timezone_module")]
        public TimeZoneData TimeZone { get; set; }
    }
}
