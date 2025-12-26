namespace Phahtshiu.Functions.Application.Contracts.Models;

/// <summary>
/// 訂閱資訊
/// </summary>
public class FeedInfo
{
    /// <summary>
    /// 標題
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// 連結
    /// </summary>
    public string Link { get; set; } = string.Empty;
    
    /// <summary>
    /// 發佈日期
    /// </summary>
    public DateTimeOffset PublishDate { get; set; }
}