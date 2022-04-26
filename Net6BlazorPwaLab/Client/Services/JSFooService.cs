using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Net6BlazorPwaLab.Client.Services;

public class JSFooService
{
  //# Injection Member
  readonly IJSRuntime jsr;

  //# resource
  IJSObjectReference jsModule;
  IJSObjectReference bar1, bar2;

  public JSFooService(IJSRuntime _jsr)
  {
    jsr = _jsr;
  }

  public async Task ImportAsync()
  {
    jsModule = await jsr.InvokeAsync<IJSObjectReference>("import", "./js/FooModule.js");
    bar1 = await jsModule.InvokeAsync<IJSObjectReference>("Bar", "barA");
    bar2 = await jsModule.InvokeAsync<IJSObjectReference>("Bar", "barB");
  }

  public async Task<string> CallActionAsync()
  {
    string str3 = await bar1.InvokeAsync<string>("toString");
    string str4 = await bar2.InvokeAsync<string>("toString");
    return $"{str3}:{str4}";
  }

}
