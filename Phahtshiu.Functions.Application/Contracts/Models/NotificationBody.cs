namespace Phahtshiu.Functions.Application.Contracts.Models;

/// <summary>
/// 通知訊息
/// </summary>
public class NotificationBody
{
    /// <summary>
    /// 標題
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// 訊息
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// 連結
    /// </summary>
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// 訊息群組
    /// </summary>
    public string Group { get; set; } = string.Empty;
}