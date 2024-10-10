namespace Phahtshiu.Functions.Infrastructure.Bark.Options;

/// <summary>
/// Bark 服務相關設定
/// </summary>
public class BarkOption
{
    public const string Position = "Bark";
    
    /// <summary>
    /// Bark 服務的 Key
    /// </summary>
    public string Key { get; set; } = string.Empty;
}