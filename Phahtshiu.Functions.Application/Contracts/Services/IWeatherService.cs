using Phahtshiu.Functions.Application.Contracts.Models;

namespace Phahtshiu.Functions.Application.Contracts.Services;

/// <summary>
/// 天氣預報服務
/// </summary>
public interface IWeatherService
{
    /// <summary>
    /// 取得指定縣市的今日天氣預報
    /// </summary>
    /// <param name="locationName">縣市名稱，例如「臺北市」</param>
    /// <param name="cancellationToken"></param>
    Task<WeatherForecastInfo?> GetTodayForecastAsync(
        string locationName,
        CancellationToken cancellationToken = default);
}
