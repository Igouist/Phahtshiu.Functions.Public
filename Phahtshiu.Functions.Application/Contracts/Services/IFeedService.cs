using Phahtshiu.Functions.Application.Common.Enums;
using Phahtshiu.Functions.Application.Contracts.Models;

namespace Phahtshiu.Functions.Application.Contracts.Services;

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