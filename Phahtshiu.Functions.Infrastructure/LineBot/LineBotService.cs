using Microsoft.Extensions.Options;
using Phahtshiu.Functions.Application.Contracts.Services;
using Phahtshiu.Functions.Infrastructure.LineBot.Options;
using LineBot = isRock.LineBot;

namespace Phahtshiu.Functions.Infrastructure.LineBot;

/// <summary>
/// LineBot 服務實作
/// </summary>
public class LineBotService(IOptions<LineBotOption> lineBotOptions) : ILineBotService
{
    private readonly isRock.LineBot.Bot _bot = new(lineBotOptions.Value.ChannelAccessToken);

    /// <summary>
    /// 回覆 LineBot 訊息
    /// </summary>
    public Task ReplyMessageAsync(string replyToken, string message)
    {
        var messages = new List<isRock.LineBot.MessageBase>
        {
            new isRock.LineBot.TextMessage(message)
        };
        _bot.ReplyMessage(replyToken, messages);
        return Task.CompletedTask;
    }
}
