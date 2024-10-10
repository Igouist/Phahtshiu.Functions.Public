namespace Phahtshiu.Functions.Infrastructure.Bark.Models;

/// <summary>
/// Bark 回應訊息
/// </summary>
public class BarkNotificationResponse
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Timestamp { get; set; }
}