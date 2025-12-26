using System.Text.Json;
using System.Text.Json.Nodes;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Phahtshiu.Functions.Application.UseCases.Github;
using Phahtshiu.Functions.Shared.Extensions;

namespace Phahtshiu.Functions.Endpoints;

public class GitHubEndpoints(IMediator mediator)
{
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
        var allowedEvents = EventHandlers.Keys;
        
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
        
        var payload = JsonSerializer.Deserialize<JsonObject>(body);
        if (payload is null)
        {
            return "無法解析訊息，中斷處理";
        }
        
        var command = EventHandlers[eventType](payload);
        await mediator.Send(command);
        return command;
    }
    
    private static readonly Dictionary<string, Func<JsonObject, IRequest>> EventHandlers = new()
    {
        ["push"] = CreatePushedEventCommand,
        ["issue_comment"] = CreateIssueCommentEventCommand
        
    };
    private static GitHubPushedEventCommand CreatePushedEventCommand(JsonObject payload) 
    => new (
        RepositoryName: payload["repository"]?["name"]?.ToString() ?? string.Empty,
        CommitMessage: payload["head_commit"]?["message"]?.ToString() ?? string.Empty,
        PusherName: payload["pusher"]?["name"]?.ToString() ?? string.Empty,
        RepositoryUrl: payload["repository"]?["url"]?.ToString() ?? string.Empty
    );

    private static IRequest CreateIssueCommentEventCommand(JsonObject payload)
    => new GithubIssueCommentedEventCommand(
        RepositoryName: payload["repository"]?["name"]?.ToString() ?? string.Empty,
        IssueTitle: payload["issue"]?["title"]?.ToString() ?? string.Empty,
        CommenterName: payload["comment"]?["user"]?["login"]?.ToString() ?? string.Empty,
        CommentBody: payload["comment"]?["body"]?.ToString() ?? string.Empty,
        IssueUrl: payload["comment"]?["html_url"]?.ToString() ?? string.Empty
    );
}