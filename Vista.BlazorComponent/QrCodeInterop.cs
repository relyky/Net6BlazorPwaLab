using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Vista.BlazorComponent;

public class QRCodeInterop : IAsyncDisposable
{
  #region Resource
  readonly Lazy<Task<IJSObjectReference>> moduleTask;
  readonly Lazy<DotNetObjectReference<QRCodeInterop>> dotNetObject;
  
  public event EventHandler<ScanResponseEvnetArgs> OnScanResponseEvnet;
  public class ScanResponseEvnetArgs : EventArgs
  {
    public string type = string.Empty;
    public string message = String.Empty;
  }
  #endregion

  //## State
  IJSObjectReference qrcReader;

  public QRCodeInterop(IJSRuntime jsr)
  {
    moduleTask = new(() => jsr.InvokeAsync<IJSObjectReference>(
       "import", "./_content/Vista.BlazorComponent/tools/QRCodeModule.js").AsTask());

    dotNetObject = new(DotNetObjectReference.Create(this));
  }

  public async ValueTask DisposeAsync()
  {
    if (qrcReader != null)
    {
      await qrcReader.DisposeAsync();
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

  public async Task StartScanAsync(string elementId, bool f_readStop)
  {
    if (qrcReader != null)
      await qrcReader.DisposeAsync();

    var fileModule = await moduleTask.Value;
    qrcReader = await fileModule.InvokeAsync<IJSObjectReference>("QRCodeModule", elementId);
    await qrcReader.InvokeVoidAsync("startScan", dotNetObject.Value, f_readStop);
  }

  public async Task StopScanAsync()
  {
    if (qrcReader == null) return;
    await qrcReader.InvokeVoidAsync("stopScan", dotNetObject.Value);
  }

  [JSInvokable("OnScanResponse")]
  public Task<int> HandleScanResponse(string type, string message)
  {
    OnScanResponseEvnet?.Invoke(this, new ScanResponseEvnetArgs
    {
      type = type,
      message = message
    });

    return Task.FromResult(0);
  }
}
