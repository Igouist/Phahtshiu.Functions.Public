namespace Phahtshiu.Functions.Application.Contracts.Services;

/// <summary>
/// LineBot 服務
/// </summary>
public interface ILineBotService
{
    /// <summary>
    /// 回覆 LineBot 訊息
    /// </summary>
    /// <param name="replyToken">回覆 Token</param>
    /// <param name="message">回覆訊息</param>
    Task ReplyMessageAsync(string replyToken, string message);
}
