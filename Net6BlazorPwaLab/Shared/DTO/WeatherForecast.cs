namespace Net6BlazorPwaLab.DTO;

public class WeatherForecast
{
  public DateTime Date { get; set; }

  public int TemperatureC { get; set; }

  public string? Summary { get; set; }

  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class WeatherForecastQryArgs
{
  public int Count { get; set; } = 300;
}
