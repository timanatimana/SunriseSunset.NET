using System.Text.Json;
using WebApi.Models.SunriseSunset;
using WebApi.Utils;
using static WebApi.Utils.CommonUtils;

namespace WebApi.Services
{
    public class SunriseSunsetService : ISunriseSunsetService
    {
        private readonly IHttpClientFactory _httpFactory;

        public SunriseSunsetService(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
    }

        public async Task<SunriseSunset?> GetSunsetSunriseAsync(SunriseSunsetRequestData reqData)
        {
            var client = _httpFactory.CreateClient();
            var url = CommonUtils.BuildSunriseSunsetUrl(reqData.Coordinates, reqData.DateString, reqData.Timezone);

            var response = await client.GetAsync(url);

            var json = response.Content.ReadAsStringAsync().Result;
            var sunsetSunriseResponse = JsonSerializer.Deserialize<SunriseSunsetResponse>(json);
            
            if (sunsetSunriseResponse is not null && sunsetSunriseResponse.Result is not null)
            {
                return new SunriseSunset()
                {
                    Sunrise = TimeOnly.Parse(sunsetSunriseResponse.Result.Sunrise),
                    Sunset = TimeOnly.Parse(sunsetSunriseResponse.Result.Sunset),
                    FirstLight = TimeOnly.Parse(sunsetSunriseResponse.Result.FirstLight),
                    LastLight = TimeOnly.Parse(sunsetSunriseResponse.Result.LastLight),
                    Dawn = TimeOnly.Parse(sunsetSunriseResponse.Result.Dawn),
                    Dusk = TimeOnly.Parse(sunsetSunriseResponse.Result.Dusk),
                    SolarNoon = TimeOnly.Parse(sunsetSunriseResponse.Result.SolarNoon),
                    GoldenHour = TimeOnly.Parse(sunsetSunriseResponse.Result.GoldenHour),
                    DayLength = TimeOnly.Parse(sunsetSunriseResponse.Result.DayLength),
                    Timezone = (Timezone) Enum.Parse(typeof(Timezone), sunsetSunriseResponse.Result.Timezone),
                    DateString = reqData.DateString
                };
            }

            return null;
        }
    }
}
