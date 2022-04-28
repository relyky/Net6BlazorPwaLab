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
    /// ���աG�D���ݦ�y RPC (server-side streaming RPC)
    /// </summary>
    public override async Task DownloadCv(DownloadByName request
      , IServerStreamWriter<Candidate> responseStream
      , ServerCallContext context)
    {
      // ����
      RepeatedField<Job> jobList = new();
      jobList.Add(new Job { Title = "���U�@�h", Salary = 17859, JobDescription = "�Ĥ@��" });
      jobList.Add(new Job { Title = "���ǱЮv", Salary = 8716, JobDescription = "�ĤG��" });
      jobList.Add(new Job { Title = "�n�����ε{���u�{�v", Salary = 8405, JobDescription = "�ĤT��" });
      jobList.Add(new Job { Title = "�q�u", Salary = 8021, JobDescription = "�ĥ|��" });
      jobList.Add(new Job { Title = "�I�u�g�z", Salary = 7145, JobDescription = "�Ĥ���" });
      jobList.Add(new Job { Title = "��u�u�K", Salary = 6812, JobDescription = "�Ĥ���" });
      jobList.Add(new Job { Title = "���ݹX�u�M����ޤu", Salary = 6335, JobDescription = "�ĤC��" });
      jobList.Add(new Job { Title = "����ޤu", Salary = 5861, JobDescription = "�ĤK��" });
      jobList.Add(new Job { Title = "�T�����קޤu", Salary = 5292, JobDescription = "�ĤE��" });
      jobList.Add(new Job { Title = "�j�����v", Salary = 5042, JobDescription = "�ĤQ��" });

      // �N�C����Ƴv�@�z�L WriteAsync ��X
      Candidate new1 = new() { Name = $"{request.Name}#1" };
      new1.Jobs.AddRange(jobList);
      await responseStream.WriteAsync(new1);

      // �N�C����Ƴv�@�z�L WriteAsync ��X
      Candidate new2 = new() { Name = $"{request.Name}#2" };
      new2.Jobs.AddRange(jobList);
      await responseStream.WriteAsync(new2);

      // �N�C����Ƴv�@�z�L WriteAsync ��X
      Candidate new3 = new() { Name = $"{request.Name}#3" };
      new3.Jobs.AddRange(jobList);
      await responseStream.WriteAsync(new3);
    }
  }
}