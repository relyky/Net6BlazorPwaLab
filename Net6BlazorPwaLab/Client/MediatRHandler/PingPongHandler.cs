using MediatR;
namespace Net6BlazorPwaLab.Client.MediatRHandler;

public class Ping : INotification {
  public string Msg { get; set; } = string.Empty;
}

public class Pong1 : INotificationHandler<Ping>
{
  public Task Handle(Ping notification, CancellationToken cancellationToken)
  {
    System.Diagnostics.Debug.WriteLine($">>>>>> Pong1:{notification.Msg}:@{DateTime.Now}");
    return Task.CompletedTask;
  }
}

public class Pong2 : INotificationHandler<Ping>
{
  public Task Handle(Ping notification, CancellationToken cancellationToken)
  {
    System.Diagnostics.Debug.WriteLine($">>>>>> Pong2:{notification.Msg}:@{DateTime.Now}");
    return Task.CompletedTask;
  }
}
