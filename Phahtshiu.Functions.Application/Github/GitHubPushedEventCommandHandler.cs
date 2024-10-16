﻿using MediatR;
using Phahtshiu.Functions.Application.Notification.Models;
using Phahtshiu.Functions.Application.Notification.Services;

namespace Phahtshiu.Functions.Application.Github;

public record GitHubPushedEventCommand(
    string RepositoryName,
    string CommitMessage,
    string PusherName,
    string RepositoryUrl) 
    : IRequest;

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
            Title = $"[GitHub] Pushed: {request.RepositoryName}",
            Message = $"{request.PusherName}: {request.CommitMessage}",
            Url = request.RepositoryUrl,
            Group = "GitHub"
        };
        
        return _notificationService.NotificationAsync(message);
    }
}