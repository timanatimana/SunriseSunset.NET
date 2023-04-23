using System.Text.Json.Serialization;

namespace WebApi.Models.SunriseSunset
{
    public class SunriseSunsetResponse
    {
        [JsonPropertyName("results")]
        public SunriseSunsetResultData Result { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
