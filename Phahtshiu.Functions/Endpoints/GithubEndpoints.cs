using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Phahtshiu.Functions.Endpoints;

public class GithubEndpoints
{
    private readonly IMediator _mediator;

    public GithubEndpoints(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// 接收 Github Webhook 訊息
    /// </summary>
    /// <returns></returns>
    [Function("Github-Webhook")]
    public async Task<string> ReceiveGithubWebhook(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] 
        HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        return body;
    }
}