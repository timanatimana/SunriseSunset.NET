using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionstackController : ControllerBase
    {
        private readonly IPositionstackService _positionstackService;
        private readonly ILogger<PositionstackController> _logger;

        public PositionstackController(ILogger<PositionstackController> logger, IPositionstackService positionstackService) { 
            _logger = logger;
            _positionstackService = positionstackService;
        }

        [HttpPost(Name = "GetLocationData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLocationData(string location)
        {
            try
            {
                var res = await _positionstackService.GetLocationDataAsync(location);
                
                if (res is not null && res.Count == 0)
                {
                    return NoContent();
                }

                return res is null ? NotFound() : Ok(res);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Could not get location data from external positionstack api - {ex.Message}");
                return BadRequest();
            }

        }
    }
}
