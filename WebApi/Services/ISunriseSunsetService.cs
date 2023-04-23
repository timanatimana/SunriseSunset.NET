using WebApi.Models.SunriseSunset;

namespace WebApi.Services
{
    public interface ISunriseSunsetService
    {
        Task<SunriseSunset?> GetSunsetSunriseAsync(SunriseSunsetRequestData reqData);
    }
}
