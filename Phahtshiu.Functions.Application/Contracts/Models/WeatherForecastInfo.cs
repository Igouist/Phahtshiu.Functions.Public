namespace Phahtshiu.Functions.Application.Contracts.Models;

/// <summary>
/// 天氣預報資訊
/// </summary>
public class WeatherForecastInfo
{
    /// <summary>
    /// 縣市名稱
    /// </summary>
    public string LocationName { get; set; } = string.Empty;

    /// <summary>
    /// 天氣現象描述，例如「晴時多雲」
    /// </summary>
    public string WeatherDescription { get; set; } = string.Empty;

    /// <summary>
    /// 降雨機率（百分比）
    /// </summary>
    public string RainProbability { get; set; } = string.Empty;

    /// <summary>
    /// 最低溫度（攝氏）
    /// </summary>
    public string MinTemperature { get; set; } = string.Empty;

    /// <summary>
    /// 最高溫度（攝氏）
    /// </summary>
    public string MaxTemperature { get; set; } = string.Empty;
}
