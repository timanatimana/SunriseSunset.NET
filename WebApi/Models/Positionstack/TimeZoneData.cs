using System.Text.Json.Serialization;

namespace WebApi.Models.Positionstack
{
    public class TimeZoneData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
