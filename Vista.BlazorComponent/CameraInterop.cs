using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Vista.BlazorComponent;

public sealed class CameraInterop : IAsyncDisposable
{
  #region Resource
  readonly Lazy<Task<IJSObjectReference>> moduleTask;
  readonly Lazy<DotNetObjectReference<CameraInterop>> dotNetObject;

  public event EventHandler<CameraResponseEvnetArgs> OnCameraResponseEvnet;
  public class CameraResponseEvnetArgs : EventArgs
  {
    public string type = string.Empty;
    public string message = string.Empty;
  }
  #endregion

  #region State
  public bool IsOpen { get; private set; } = false;
  #endregion

  public CameraInterop(IJSRuntime jsRuntime)
  {
    moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
       "import", "./_content/Vista.BlazorComponent/tools/cameraTools.js").AsTask());

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

  public async Task StartCameraAsync(ElementReference videoElement)
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("startCamera", dotNetObject.Value, videoElement);
    IsOpen = true;
  }

  public async Task StopCameraAsync()
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("stopCamera", dotNetObject.Value);
    IsOpen = false;
  }

  public async Task TakePhotoAsync()
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("takePhoto", dotNetObject.Value);
  }

  [JSInvokable("OnCameraResponse")]
  public Task<int> HandleCameraResponse(string type, string message)
  {
    OnCameraResponseEvnet?.Invoke(this, new CameraResponseEvnetArgs
    {
      type = type,
      message = message
    });

    return Task.FromResult(0);
  }

}
