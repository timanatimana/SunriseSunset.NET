using Microsoft.AspNetCore.Mvc;
using WebApi.Models.SunriseSunset;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SunriseSunsetController : ControllerBase
{

    private readonly ISunriseSunsetService _sunriseSunsetService;
    private readonly ILogger<SunriseSunsetController> _logger;

    public SunriseSunsetController(ILogger<SunriseSunsetController> logger, ISunriseSunsetService sunriseSunsetService)
    {
        _logger = logger;
        _sunriseSunsetService = sunriseSunsetService;
    }

    [HttpPost(Name = "GetSunriseSunset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSunriseSunset(SunriseSunsetRequestData reqData)
    {
        try
        {
            var res = await _sunriseSunsetService.GetSunsetSunriseAsync(reqData);

            return res is null ? NotFound() : Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Could not get sunrise sunset data from external SunsetSunrise api - {ex.Message}");
            return BadRequest();
        }

    }
}
