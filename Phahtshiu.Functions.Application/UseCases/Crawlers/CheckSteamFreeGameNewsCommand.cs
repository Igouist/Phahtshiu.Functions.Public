using System.Collections.Immutable;
using MediatR;
using Phahtshiu.Functions.Application.Common.Enums;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;
using Phahtshiu.Functions.Shared.Extensions;

namespace Phahtshiu.Functions.Application.UseCases.Crawlers;

public record CheckSteamFreeGameNewsCommand : IRequest<string>;

public class CheckSteamFreeGameNewsCommandHandler(
    IFeedService feedService,
    ICheckPointService checkPointService,
    INotificationService notificationService) 
    : IRequestHandler<CheckSteamFreeGameNewsCommand, string>
{
    public async Task<string> Handle(
        CheckSteamFreeGameNewsCommand request, 
        CancellationToken cancellationToken)
    {
        const string checkPointKey = "SteamFreeGameNews";
        var lastPointTime = await checkPointService.GetCheckPointAsync(
            checkPointKey, 
            cancellationToken);
        
        var feedInfos = (await feedService.GetFeedAsync(
            FeedType.SteamFreeGames, 
            lastPointTime, 
            cancellationToken))?.ToImmutableList();
        
        if (feedInfos.IsNullOrEmpty())
        {
            return "沒有任何新消息。";
        }
        
        var newPointTime = feedInfos!.Max(x => x.PublishDate);
        await checkPointService.UpsertCheckPointAsync(
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
            .Select(notificationService.NotificationAsync);
        
        await Task.WhenAll(notificationTasks);
    }
}