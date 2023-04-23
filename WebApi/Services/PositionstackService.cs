using Microsoft.Extensions.Options;
using System.Text.Json;
using WebApi.Config;
using WebApi.Models.Positionstack;
using WebApi.Utils;

namespace WebApi.Services
{
    public class PositionstackService : IPositionstackService
    {
        private readonly IHttpClientFactory _httpFactory;
        private Positionstack _positionstackConfig;

        public PositionstackService(IHttpClientFactory httpFactory, IOptions<Positionstack> opts)
        {
            _httpFactory = httpFactory;
            _positionstackConfig = opts.Value;
        }
        public async Task<List<LocationData>?> GetLocationDataAsync(string location)
        {
            var client = _httpFactory.CreateClient();
            var url = CommonUtils.BuildPositionstackUrl(_positionstackConfig.ApiKey, location);

            var response = await client.GetAsync(url);

            var json = response.Content.ReadAsStringAsync().Result;
            var positionstackResponse = JsonSerializer.Deserialize<PositionstackResponse>(json);

            if (positionstackResponse is not null && positionstackResponse.Result is not null)
            {
                var result = new List<LocationData>();

                int count = 0;
                foreach (var data in positionstackResponse.Result)
                {
                    count++;
                    var locationData = new LocationData()
                    {
                        Id = count,
                        Latitude = data.Latitude,
                        Longitude = data.Longitude,
                        Name = data.Name,
                        Region = data.Region,
                        Country = data.Country,
                        Continent = data.Continent,
                        Label = data.Label,
                        TimezoneString = data.TimeZone.Name,
                    };

                    result.Add(locationData);
                }

                return result;
            }

            return null;
        }
    }
}
