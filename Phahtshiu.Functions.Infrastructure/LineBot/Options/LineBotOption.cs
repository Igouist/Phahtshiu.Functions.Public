namespace Phahtshiu.Functions.Infrastructure.LineBot.Options;

/// <summary>
/// LineBot 選項（Infrastructure 層）
/// </summary>
public class LineBotOption
{
    public const string Position = "LineBot";
    public string ChannelAccessToken { get; set; } = string.Empty;
}
