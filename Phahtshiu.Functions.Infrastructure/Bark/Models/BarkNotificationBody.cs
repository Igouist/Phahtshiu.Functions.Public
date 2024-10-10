namespace Phahtshiu.Functions.Infrastructure.Bark.Models;

/// <summary>
/// Bark 通知訊息
/// </summary>
/// <see ref="https://bark.day.app/#/tutorial"/>
public class BarkNotificationBody
{
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    
    public string? Group { get; set; }
    public string? Url { get; set; }
}