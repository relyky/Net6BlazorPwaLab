using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vista.BlazorComponent;

public sealed class GeoLocationInterop : IAsyncDisposable
{
  #region Resource
  readonly Lazy<Task<IJSObjectReference>> moduleTask;
  readonly Lazy<DotNetObjectReference<GeoLocationInterop>> dotNetObject;
  #endregion

  #region nest class
  /// <summary>
  /// Geolocation Position, based on <see href="https://developer.mozilla.org/en-US/docs/Web/API/GeolocationPosition"/>.
  /// </summary>
  public class GeolocationPosition
  {
    /// <summary>
    /// The coordinates defining the current location
    /// </summary>
    public GeolocationCoordinates Coords { get; set; }

    /// <summary>
    /// The time the coordinates were taken, in milliseconds since the Unix epoch.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// The <see cref="DateTimeOffset"/> derived from the <see cref="Timestamp"/>, in UTC.
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset DateTimeOffset => DateTimeOffset.FromUnixTimeMilliseconds(Timestamp);
  }

  /// <summary>
  /// Geolocation Coordinates, based on <see href="https://developer.mozilla.org/en-US/docs/Web/API/GeolocationCoordinates"/>.
  /// </summary>
  public class GeolocationCoordinates
  {
    /// <summary>
    /// Latitude in decimal degrees.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Longitude in decimal degrees.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Altitude in metres, relative to sea level.
    /// </summary>
    public double? Altitude { get; set; }

    /// <summary>
    /// Accuracy of the latitude and longitude properties, in metres.
    /// </summary>
    public double Accuracy { get; set; }

    /// <summary>
    /// Accuracy of the altitude, in metres.
    /// </summary>
    public double? AltitudeAccuracy { get; set; }

    /// <summary>
    /// The direction the device is travelling, in degrees clockwise from true north.
    /// </summary>
    public double? Heading { get; set; }

    /// <summary>
    /// The velocity of the device, in metres per second.
    /// </summary>
    public double? Speed { get; set; }
  }
  #endregion

  public GeoLocationInterop(IJSRuntime jsr)
  {
    moduleTask = new(() => jsr.InvokeAsync<IJSObjectReference>(
       "import", "./_content/Vista.BlazorComponent/tools/geoTool.js").AsTask());

    dotNetObject = new(DotNetObjectReference.Create(this));
  }

  #region Dispose
  public async ValueTask DisposeAsync()
  {
    if (moduleTask.IsValueCreated)
    {
      var module = await moduleTask.Value;
      await module.DisposeAsync();
    }

    if (dotNetObject.IsValueCreated)
    {
      dotNetObject.Value.Dispose();
    }
  }
  #endregion

  public async Task<GeolocationPosition> getLocationAsync()
  {
    try
    {
      var module = await moduleTask.Value;
      var position = await module.InvokeAsync<GeolocationPosition>("getLocation", dotNetObject.Value);
      return position;
    }
    catch (JSException ex)
    {
      throw new ApplicationException(ex.Message, ex);
    }
  }
}
