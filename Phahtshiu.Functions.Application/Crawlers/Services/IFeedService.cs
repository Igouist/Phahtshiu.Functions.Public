using Phahtshiu.Functions.Application.Crawlers.Enums;
using Phahtshiu.Functions.Application.Crawlers.Models;

namespace Phahtshiu.Functions.Application.Crawlers.Services;

/// <summary>
/// 訂閱消息服務
/// </summary>
public interface IFeedService
{
    /// <summary>
    /// 取得訂閱消息
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<FeedInfo>> GetFeedAsync(
        FeedType feedType,
        DateTimeOffset? minPublishTime = null,
        CancellationToken cancellationToken = default);
}