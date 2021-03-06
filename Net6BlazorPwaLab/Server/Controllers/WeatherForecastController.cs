using Microsoft.AspNetCore.Mvc;
using Net6BlazorPwaLab.DTO;

namespace Net6BlazorPwaLab.Server.Controllers
{
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

    //[HttpGet]
    //public IEnumerable<WeatherForecast> Get()
    //{
    //  return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //  {
    //    Date = DateTime.Now.AddDays(index),
    //    TemperatureC = Random.Shared.Next(-20, 55),
    //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //  })
    //  .ToArray();
    //}

    [HttpPost("[action]")]
    public List<WeatherForecast> QryDataList(WeatherForecastQryArgs args)
    {
      return Enumerable.Range(1, args.Count).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      }).ToList();
    }
  }
}