using MediatR;
namespace Net6BlazorPwaLab.Server.MediatRHandler;

public class QueryUserHandler : IRequestHandler<QueryUserRequest, QueryUserResponse>
{
    public Task<QueryUserResponse> Handle(QueryUserRequest request, CancellationToken cancellationToken)
    {
      return Task.FromResult(new QueryUserResponse { 
        DisplayName = $"我是{request.Id}的名字"      
      });
    }
}

public class QueryUserResponse
{
  public string DisplayName { get; set; } = string.Empty;
}

public class QueryUserRequest : IRequest<QueryUserResponse>
{
  public int Id { get; set; }
}