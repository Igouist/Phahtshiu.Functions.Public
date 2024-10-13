namespace Phahtshiu.Functions.Options;

/// <summary>
/// 定時提醒相關設定
/// </summary>
public class ReminderOption
{
    public const string Position = "Reminder";
    
    /// <summary>
    /// 訂便當的連結
    /// </summary>
    public string BentoLink { get; set; } = string.Empty;
}