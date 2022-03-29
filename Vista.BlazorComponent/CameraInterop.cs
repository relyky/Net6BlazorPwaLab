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
  IJSObjectReference cameraEntity;
  #endregion

  public CameraInterop(IJSRuntime jsr)
  {
    moduleTask = new(() => jsr.InvokeAsync<IJSObjectReference>(
       "import", "./_content/Vista.BlazorComponent/tools/CameraModule.js").AsTask());

    dotNetObject = new(DotNetObjectReference.Create(this));
  }

  #region Dispose
  public async ValueTask DisposeAsync()
  {
    if (cameraEntity != null)
    {
      await cameraEntity.DisposeAsync();
    }

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
    if (cameraEntity != null)
      await cameraEntity.DisposeAsync();

    var fileModule = await moduleTask.Value;
    cameraEntity = await fileModule.InvokeAsync<IJSObjectReference>("CameraModule", dotNetObject.Value, videoElement);
    await cameraEntity.InvokeVoidAsync("startCamera");
    IsOpen = true;
  }

  public async Task StopCameraAsync()
  {
    if (cameraEntity == null) return;
    await cameraEntity.InvokeVoidAsync("stopCamera");
    IsOpen = false;
  }

  public async Task TakePhotoAsync()
  {
    if (cameraEntity == null) return;
    await cameraEntity.InvokeVoidAsync("takePhoto");
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
