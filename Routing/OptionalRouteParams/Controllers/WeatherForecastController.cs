using Microsoft.AspNetCore.Mvc;

namespace OptionalRouteParams.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    //optional route parameters must come at the last
    [HttpGet("{color}/{id:int?}/{name?}")]
    public IActionResult GenMessage(string color, int id = 1, string? name = null)
    {
        return Ok($"{color} {id} {name ?? ""}");
    }
}