using Microsoft.JSInterop;

namespace Vista.BlazorComponent;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class PdfToolJsInterop : IAsyncDisposable
{
  private readonly Lazy<Task<IJSObjectReference>> moduleTask;

  public PdfToolJsInterop(IJSRuntime jsRuntime)
  {
    moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
       "import", "./_content/Vista.BlazorComponent/pdfToolJsInterop.js").AsTask());
  }

  public async Task InitAsync()
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("init");
  }

  public async Task RenderPdfAsync(string url)
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("renderPdf", url);
  }

  public async ValueTask DisposeAsync()
  {
    if (moduleTask.IsValueCreated)
    {
      var module = await moduleTask.Value;
      await module.DisposeAsync();
    }
  }
}