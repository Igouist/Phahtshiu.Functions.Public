using System.Text.Json;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Phahtshiu.Functions.Application.Github;
using Phahtshiu.Functions.Models;
using Phahtshiu.Functions.Shared.Extensions;

namespace Phahtshiu.Functions.Endpoints;

public class GitHubEndpoints
{
    private readonly IMediator _mediator;

    public GitHubEndpoints(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// 接收 Github Webhook 訊息
    /// </summary>
    /// <returns></returns>
    [Function("Github-Webhook")]
    public async Task<object> ReceiveGithubWebhook(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] 
        HttpRequestData req)
    {
        var headers = req.Headers;
        if (headers.TryGetValues("X-GitHub-Event", out var githubEvents) is false)
        {
            return "無法取得事件類型，中斷處理";
        }
        
        var eventType = githubEvents.FirstOrDefault();
        var allowedEvents = _eventHandlers.Keys;
        
        if (eventType is null || 
            allowedEvents.Contains(eventType) is false)
        {
            return $"不支援的事件類型: {eventType}，中斷處理";
        }
        
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        if (body.IsNullOrEmpty())
        {
            return "未收到訊息，中斷處理";
        }
        
        var payload = JsonSerializer.Deserialize<GitHubWebhookPayload>(body);
        if (payload is null)
        {
            return "無法解析訊息，中斷處理";
        }
        
        var command = _eventHandlers[eventType](payload);
        return command;
    }
    
    private static Dictionary<string, Func<GitHubWebhookPayload, IRequest>> _eventHandlers = new()
    {
        ["push"] = CreatePushedEventCommand
    };
    
    private static GitHubPushedEventCommand CreatePushedEventCommand(GitHubWebhookPayload payload) 
    => new (
        RepositoryName: payload.Repository.Name,
        CommitMessage: payload.HeadCommit.Message,
        PusherName: payload.Pusher.Name
    );
}