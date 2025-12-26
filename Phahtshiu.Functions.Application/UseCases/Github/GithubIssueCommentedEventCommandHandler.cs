using MediatR;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;

namespace Phahtshiu.Functions.Application.UseCases.Github;

public record GithubIssueCommentedEventCommand(
    string RepositoryName,
    string IssueTitle,
    string CommenterName,
    string CommentBody,
    string IssueUrl
    ) : IRequest;

public class GithubIssueCommentedEventCommandHandler(
    INotificationService notificationService) 
    : IRequestHandler<GithubIssueCommentedEventCommand>
{
    public Task Handle(
        GithubIssueCommentedEventCommand request, 
        CancellationToken cancellationToken)
    {
        var message = new NotificationBody
        {
            Title = $"[GitHub] Issue: {request.IssueTitle} - {request.RepositoryName}",
            Message = $"{request.CommenterName}: {request.CommentBody}",
            Url = request.IssueUrl,
            Group = "GitHub"
        };
        
        return notificationService.NotificationAsync(message);
    }
}