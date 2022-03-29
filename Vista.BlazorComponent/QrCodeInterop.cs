using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Vista.BlazorComponent;

public class QrCodeInterop : IAsyncDisposable
{
  #region Resource
  readonly Lazy<Task<IJSObjectReference>> moduleTask;
  readonly Lazy<DotNetObjectReference<QrCodeInterop>> dotNetObject;
  
  public event EventHandler<ScanResponseEvnetArgs> OnScanResponseEvnet;
  public class ScanResponseEvnetArgs : EventArgs
  {
    public string type = string.Empty;
    public string message = String.Empty;
  }
  #endregion

  //## State
  IJSObjectReference qrcodeModule;

  public QrCodeInterop(IJSRuntime jsRuntime)
  {
    moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
       "import", "./_content/Vista.BlazorComponent/tools/QRCodeModule.js").AsTask());

    dotNetObject = new(DotNetObjectReference.Create(this));
  }

  public async ValueTask DisposeAsync()
  {
    if (qrcodeModule != null)
    {
      await qrcodeModule.DisposeAsync();
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

  //public async ValueTask<string> ScanQrCodeOnce(string elementId)
  //{
  //  var module = await moduleTask.Value;
  //  return await module.InvokeAsync<string>("scanQrCodeOnce", elementId);
  //}

  public async Task StartScanAsync(string elementId, bool f_readStop)
  {
    var fileModule = await moduleTask.Value;
    qrcodeModule = await fileModule.InvokeAsync<IJSObjectReference>("QRCodeModule", elementId);
    await qrcodeModule.InvokeVoidAsync("startScan", dotNetObject.Value, f_readStop);
  }

  public async Task StopScanAsync()
  {
    if (qrcodeModule == null) return;
    await qrcodeModule.InvokeVoidAsync("stopScan", dotNetObject.Value);
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
