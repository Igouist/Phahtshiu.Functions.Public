namespace Phahtshiu.Functions.Options;

public class LineBotOption
{
    public const string Position = "LineBot";
    public string ChannelId { get; set; }
    public string ChannelSecret { get; set; } 
    public string ChannelAccessToken { get; set; }
    public string UserId { get; set; }
}