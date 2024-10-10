using MediatR;
using Phahtshiu.Functions.Application.Notification.Models;
using Phahtshiu.Functions.Application.Notification.Services;

namespace Phahtshiu.Functions.Application.Github;

public record GitHubPushedEventCommand(
    string RepositoryName,
    string CommitMessage,
    string PusherName) : IRequest;

public class GitHubPushedEventCommandHandler : IRequestHandler<GitHubPushedEventCommand>
{
    private readonly INotificationService _notificationService;

    public GitHubPushedEventCommandHandler(
        INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public Task Handle(
        GitHubPushedEventCommand request, 
        CancellationToken cancellationToken)
    {
        var message = new NotificationBody
        {
            Title = $"GitHub Pushed: {request.RepositoryName}",
            Message = $"{request.CommitMessage} by {request.PusherName}",
            Group = "GitHub"
        };
        
        return _notificationService.NotificationAsync(message);
    }
}