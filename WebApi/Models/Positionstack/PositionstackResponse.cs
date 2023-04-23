using System.Text.Json.Serialization;

namespace WebApi.Models.Positionstack
{
    public class PositionstackResponse
    {
        [JsonPropertyName("data")]
        public List<PositionstackResultData> Result { get; set; }
    }
}
