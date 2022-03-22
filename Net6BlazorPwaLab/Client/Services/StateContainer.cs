namespace Net6BlazorPwaLab.Client.Services;

public class StateContainer
{
  string? savedString;
  int changedCount = 0;

  public string Property
  {
    get => savedString ?? string.Empty;
    set
    {
      savedString = value;
      changedCount++;
      NotifyStateChanged();
    }
  }

  public int ChangedCount => changedCount;

  public event Action? OnChange;

  public void NotifyStateChanged() => OnChange?.Invoke();
}
