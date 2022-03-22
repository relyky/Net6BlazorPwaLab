namespace Net6BlazorPwaLab.Models;

public class NotifyMessage
{
  public string Message { get; set; } = string.Empty;
  public MudBlazor.Severity Severity  { get; set; } = MudBlazor.Severity.Info;
}