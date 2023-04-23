using WebApi.Models.Positionstack;

namespace WebApi.Services
{
    public interface IPositionstackService
    {
        Task<List<LocationData>?> GetLocationDataAsync(string location);
    }
}
