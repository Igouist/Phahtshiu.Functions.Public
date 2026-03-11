using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;
using Phahtshiu.Functions.Infrastructure.Weather.Models;
using Phahtshiu.Functions.Infrastructure.Weather.Options;

namespace Phahtshiu.Functions.Infrastructure.Weather;

/// <summary>
/// 透過中央氣象署 (CWA) Open Data API 取得天氣預報
/// </summary>
public class CwaWeatherService(
    ILogger<CwaWeatherService> logger,
    IOptions<CwaOption> cwaOptions,
    IHttpClientFactory httpClientFactory) : IWeatherService
{
    // https://opendata.cwa.gov.tw/dist/opendata-swagger.html
    private const string ForecastApiUrl =
        "https://opendata.cwa.gov.tw/api/v1/rest/datastore/F-C0032-001";

    public async Task<WeatherForecastInfo?> GetTodayForecastAsync(
        string locationName,
        CancellationToken cancellationToken = default)
    {
        var apiKey = cwaOptions.Value.ApiKey;
        var url = $"{ForecastApiUrl}?Authorization={apiKey}&locationName={Uri.EscapeDataString(locationName)}";

        using var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync(url, cancellationToken);

        if (response.IsSuccessStatusCode is false)
        {
            logger.LogError("CWA API 呼叫失敗，狀態碼：{StatusCode}", response.StatusCode);
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<CwaForecastResponse>(
            cancellationToken: cancellationToken);

        var location = result?.Records?.Locations
            .FirstOrDefault(l => l.LocationName == locationName);

        if (location is null)
        {
            logger.LogWarning("CWA API 回應中找不到縣市：{LocationName}", locationName);
            return null;
        }

        // F-C0032-001 每個 element 有 3 個時段（0~12h、12~24h、24~36h），取第一個時段（今日白天）
        var elements = location.WeatherElements
            .ToDictionary(e => e.ElementName, e => e.Times.FirstOrDefault()?.Parameter?.ParameterName ?? string.Empty);

        return new WeatherForecastInfo
        {
            LocationName    = location.LocationName,
            WeatherDescription = elements.GetValueOrDefault("Wx", string.Empty),
            RainProbability    = elements.GetValueOrDefault("PoP", string.Empty),
            MinTemperature     = elements.GetValueOrDefault("MinT", string.Empty),
            MaxTemperature     = elements.GetValueOrDefault("MaxT", string.Empty),
        };
    }
}
