using Grpc.Net.Client;
using MyGrpcService;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World! MyGrpcApp. ");
Console.WriteLine("Press any to go.");
Console.ReadKey();

Console.WriteLine(">>>>>> GO");

//using var channel = GrpcChannel.ForAddress("http://localhost:5027");
using var channel = GrpcChannel.ForAddress("https://localhost:7027");
var client = new Greeter.GreeterClient(channel);

var result = client.SayHello(new HelloRequest { Name = "Foo" });

Console.WriteLine($"result: {result.Message}");

Console.WriteLine(">>>>>> EOF");
