using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Phahtshiu.Functions.Application.Common.Enums;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;
using Phahtshiu.Functions.Infrastructure.Crawlers.Options;
using Phahtshiu.Functions.Shared.Extensions;

namespace Phahtshiu.Functions.Infrastructure.Crawlers;

/// <summary>
/// 訂閱消息服務
/// </summary>
public class FeedService : IFeedService 
{
    private readonly ILogger<FeedService> _logger;
    private readonly HttpClient _httpClient;
    private readonly FeedSourceOption _feedSourceOption;
    
    public FeedService(
        ILogger<FeedService> logger,
        IHttpClientFactory httpClientFactory,
        IOptions<FeedSourceOption> feedSourceOption)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
        _feedSourceOption = feedSourceOption.Value;
    }
    
    private static readonly Dictionary<FeedType, Func<FeedSourceOption, string>> FeedSourceMap = new()
    {
        [FeedType.SteamFreeGames] = x => x.SteamFreeGames,
    };
    
    public async Task<IEnumerable<FeedInfo>> GetFeedAsync(
        FeedType feedType, 
        DateTimeOffset? minPublishTime = null,
        CancellationToken cancellationToken = default)
    {
        var isFeedSourceExist = FeedSourceMap.TryGetValue(feedType, out var feedSource);
        if (isFeedSourceExist is false) 
        {
            throw new NotSupportedException($"FeedType {feedType} is not supported.");
        }
        
        var feedUrl = feedSource?.Invoke(_feedSourceOption) ?? string.Empty;
        if (feedUrl.IsNullOrWhiteSpace())
        {
            _logger.LogWarning("FeedSource {feedType} is not configured.", feedType);
            throw new NotSupportedException($"FeedType {feedType} is not configured.");
        }
        
        try
        {
            await using var responseStream = await _httpClient.GetStreamAsync(feedUrl, cancellationToken);
            using var xmlReader = XmlReader.Create(responseStream);
            
            var feed = SyndicationFeed.Load(xmlReader);
            if (feed is null || feed.Items.IsNullOrEmpty())
            {
                return Enumerable.Empty<FeedInfo>();
            }

            var newItems = feed.Items
                .Where(item => item.PublishDate.UtcDateTime > minPublishTime)
                .Select(item => new FeedInfo
                {
                    Title = item.Title.Text,
                    Link = item.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty,
                    PublishDate = item.PublishDate.UtcDateTime,
                });
            
            return newItems;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching or parsing RSS feed: {feedUrl}", feedUrl);
            throw;
        }
    }
}