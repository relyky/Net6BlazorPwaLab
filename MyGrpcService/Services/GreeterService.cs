using Google.Protobuf.Collections;
using Grpc.Core;
using MyGrpcService;

namespace MyGrpcService.Services
{
  public class GreeterService : Greeter.GreeterBase
  {
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
      _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
      return Task.FromResult(new HelloReply
      {
        Message = "Hello " + request.Name
      });
    }

    /// <summary>
    /// 測試：主機端串流 RPC (server-side streaming RPC)
    /// </summary>
    public override async Task DownloadCv(DownloadByName request
      , IServerStreamWriter<Candidate> responseStream
      , ServerCallContext context)
    {
      // 模擬
      RepeatedField<Job> jobList = new();
      jobList.Add(new Job { Title = "註冊護士", Salary = 17859, JobDescription = "第一位" });
      jobList.Add(new Job { Title = "中學教師", Salary = 8716, JobDescription = "第二位" });
      jobList.Add(new Job { Title = "軟件應用程式工程師", Salary = 8405, JobDescription = "第三位" });
      jobList.Add(new Job { Title = "電工", Salary = 8021, JobDescription = "第四位" });
      jobList.Add(new Job { Title = "施工經理", Salary = 7145, JobDescription = "第五位" });
      jobList.Add(new Job { Title = "木工工匠", Salary = 6812, JobDescription = "第六位" });
      jobList.Add(new Job { Title = "金屬鉗工和機械技工", Salary = 6335, JobDescription = "第七位" });
      jobList.Add(new Job { Title = "水喉技工", Salary = 5861, JobDescription = "第八位" });
      jobList.Add(new Job { Title = "汽車維修技工", Salary = 5292, JobDescription = "第九位" });
      jobList.Add(new Job { Title = "大學講師", Salary = 5042, JobDescription = "第十位" });

      // 將每筆資料逐一透過 WriteAsync 輸出
      Candidate new1 = new() { Name = $"{request.Name}#1" };
      new1.Jobs.AddRange(jobList);
      await responseStream.WriteAsync(new1);

      // 將每筆資料逐一透過 WriteAsync 輸出
      Candidate new2 = new() { Name = $"{request.Name}#2" };
      new2.Jobs.AddRange(jobList);
      await responseStream.WriteAsync(new2);

      // 將每筆資料逐一透過 WriteAsync 輸出
      Candidate new3 = new() { Name = $"{request.Name}#3" };
      new3.Jobs.AddRange(jobList);
      await responseStream.WriteAsync(new3);
    }
  }
}