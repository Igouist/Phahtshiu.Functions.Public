using System.Text.Json.Serialization;

namespace Phahtshiu.Functions.Infrastructure.Weather.Models;

/// <summary>
/// CWA F-C0032-001 今明 36 小時天氣預報 API 回應
/// </summary>
public class CwaForecastResponse
{
    [JsonPropertyName("success")]
    public string Success { get; set; } = string.Empty;

    [JsonPropertyName("records")]
    public CwaForecastRecords? Records { get; set; }
}

public class CwaForecastRecords
{
    [JsonPropertyName("location")]
    public List<CwaForecastLocation> Locations { get; set; } = [];
}

public class CwaForecastLocation
{
    [JsonPropertyName("locationName")]
    public string LocationName { get; set; } = string.Empty;

    [JsonPropertyName("weatherElement")]
    public List<CwaWeatherElement> WeatherElements { get; set; } = [];
}

public class CwaWeatherElement
{
    /// <summary>
    /// 要素名稱，例如 Wx（天氣現象）、PoP（降雨機率）、MinT（最低溫）、MaxT（最高溫）
    /// </summary>
    [JsonPropertyName("elementName")]
    public string ElementName { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    public List<CwaWeatherTime> Times { get; set; } = [];
}

public class CwaWeatherTime
{
    [JsonPropertyName("startTime")]
    public string StartTime { get; set; } = string.Empty;

    [JsonPropertyName("endTime")]
    public string EndTime { get; set; } = string.Empty;

    [JsonPropertyName("parameter")]
    public CwaWeatherParameter? Parameter { get; set; }
}

public class CwaWeatherParameter
{
    [JsonPropertyName("parameterName")]
    public string ParameterName { get; set; } = string.Empty;

    [JsonPropertyName("parameterUnit")]
    public string? ParameterUnit { get; set; }
}
