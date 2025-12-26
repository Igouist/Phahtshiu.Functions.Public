using System.Collections.Immutable;
using MediatR;
using Phahtshiu.Functions.Application.Common.Enums;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;
using Phahtshiu.Functions.Shared.Extensions;

namespace Phahtshiu.Functions.Application.UseCases.Crawlers;

public record CheckSteamFreeGameNewsCommand : IRequest<string>;

public class CheckSteamFreeGameNewsCommandHandler : IRequestHandler<CheckSteamFreeGameNewsCommand, string>
{
    private readonly IFeedService _feedService;
    private readonly ICheckPointService _checkPointService;
    private readonly INotificationService _notificationService;

    public CheckSteamFreeGameNewsCommandHandler(
        IFeedService feedService,
        ICheckPointService checkPointService, 
        INotificationService notificationService)
    {
        _feedService = feedService;
        _checkPointService = checkPointService;
        _notificationService = notificationService;
    }

    public async Task<string> Handle(
        CheckSteamFreeGameNewsCommand request, 
        CancellationToken cancellationToken)
    {
        const string checkPointKey = "SteamFreeGameNews";
        var lastPointTime = await _checkPointService.GetCheckPointAsync(
            checkPointKey, 
            cancellationToken);
        
        var feedInfos = (await _feedService.GetFeedAsync(
            FeedType.SteamFreeGames, 
            lastPointTime, 
            cancellationToken))?.ToImmutableList();
        
        if (feedInfos.IsNullOrEmpty())
        {
            return "沒有任何新消息。";
        }
        
        var newPointTime = feedInfos!.Max(x => x.PublishDate);
        await _checkPointService.UpsertCheckPointAsync(
            checkPointKey, 
            newPointTime, 
            cancellationToken);
        
        await SendNotificationAsync(feedInfos!, cancellationToken);
        return "發現 " + feedInfos!.Count + " 則新消息，已發送通知。";
    }
    
    private async Task SendNotificationAsync(
        IEnumerable<FeedInfo> feedInfos, 
        CancellationToken cancellationToken)
    {
        var notificationTasks = feedInfos
            .Select(feed => new NotificationBody
            {
                Title = "[Steam Free Game News]" + feed.Title,
                Message = feed.Title,
                Url = feed.Link,
                Group = "SteamFreeGameNews"
            })
            .Select(notification => _notificationService.NotificationAsync(notification));
        
        await Task.WhenAll(notificationTasks);
    }
}