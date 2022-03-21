using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;

namespace Net6BlazorPwaLab.Client.Services;

public class WebApiService
{
  readonly HttpClient http;

  public WebApiService(HttpClient _http)
  {
    http = _http;
  }

  /// <summary>
  /// JSON package only.
  /// </summary>
  public async Task<TRes> PostDataAsync<TReq, TRes>(string apiPath, TReq req)
      where TReq : class
      where TRes : class
  {
    //HttpClient http = new HttpClient();
    //http.BaseAddress = new Uri(SysEnv.WebApi2BaseAddress); // 目標 Web API URL // 由環境取得
    //http.DefaultRequestHeaders.Add("Authorization", @"Bearer xxxxxxxx"); // 認證機制 // 由環境取得

    var response = await http.PostAsJsonAsync<TReq>(apiPath, req);
    if (!response.IsSuccessStatusCode)
    {
      var msg = await response.Content.ReadAsStringAsync();
      throw new HttpRequestException(msg);
    }

    // success
    var resp = await response.Content.ReadFromJsonAsync<TRes>();
    return resp;
  }

  /// <summary>
  /// JSON package only.
  /// </summary>
  public async Task<TRes> PostDataAsync<TRes>(string apiPath)
      where TRes : class
  {
    //HttpClient http = new HttpClient();
    //http.BaseAddress = new Uri(SysEnv.WebApi2BaseAddress); // 目標 Web API URL // 由環境取得
    //http.DefaultRequestHeaders.Add("Authorization", @"Bearer xxxxxxxx"); // 認證機制 // 由環境取得

    var response = await http.PostAsync(apiPath, null);
    if (!response.IsSuccessStatusCode)
    {
      var msg = await response.Content.ReadAsStringAsync();
      throw new HttpRequestException(msg);
    }

    // success
    var resp = await response.Content.ReadFromJsonAsync<TRes>();
    return resp;
  }

  /// <summary>
  /// 上傳檔案
  /// </summary>

  /// <summary>
  /// 下載檔案
  /// </summary>
}
