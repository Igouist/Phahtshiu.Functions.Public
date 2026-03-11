namespace Phahtshiu.Functions.Infrastructure.Weather.Options;

/// <summary>
/// 中央氣象署 (CWA) Open Data 相關設定
/// </summary>
public class CwaOption
{
    public const string Position = "Cwa";

    /// <summary>
    /// CWA Open Data API Key
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// 查詢的縣市名稱，例如「臺北市」
    /// </summary>
    public string LocationName { get; set; } = "臺北市";
}
