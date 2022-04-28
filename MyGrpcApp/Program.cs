using Grpc.Core;
using Grpc.Net.Client;
using MyGrpcService;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("§§ gRPC Lab");

//using var channel = GrpcChannel.ForAddress("http://localhost:5027");
using var channel = GrpcChannel.ForAddress("https://localhost:7027");
var client = new Greeter.GreeterClient(channel);
Console.WriteLine("已建立 gRPC 連線");

while (true)
{
  Console.WriteLine(Environment.NewLine + "測試情境指令");
  Console.WriteLine("[A] 基本通訊");
  Console.WriteLine("[B] 主機端串流 gRPC (server-side streaming gRPC)");
  Console.WriteLine("[Esc] 離開");

  var key = Console.ReadKey();

  if (key.Key == ConsoleKey.A)
  {
    Console.WriteLine(Environment.NewLine + "# 基本通訊 ------------");
    var result = client.SayHello(new HelloRequest { Name = "Foo" });
    Console.WriteLine($"result: {result.Message}");
  }
  else if (key.Key == ConsoleKey.B)
  {
    Console.WriteLine(Environment.NewLine + "# 主機端串流 gRPC ------------");

    using var resp = client.DownloadCv(new DownloadByName { Name = "Bar" });
    while (resp.ResponseStream.MoveNext().Result)
    {
      var item = resp.ResponseStream.Current;
      Console.WriteLine($"{item.Name}, {item.Jobs[0].Title}, {item.Jobs[1].Title}");
    }

    //===========================================
  }
  else if (key.Key == ConsoleKey.Escape)
  {
    Console.WriteLine("Exit >>>>>>");
    break;
  }
}