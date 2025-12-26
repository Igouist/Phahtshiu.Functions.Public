using System.Text.Json;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;

namespace Phahtshiu.Functions.Infrastructure.Sportscenter;

/// <summary>
/// 運動中心相關服務
/// </summary>
public class SportscenterService(IHttpClientFactory httpClientFactory) : ISportscenterService
{
    /// <summary>
    /// 取得指定運動中心的人數資訊
    /// </summary>
    /// <param name="sportscenterName">運動中心名稱</param>
    /// <returns>該運動中心的人數資訊，找不到時回傳 null</returns>
    public async Task<SportscenterLocationPeopleCountInfo?> FetchPeopleCountAsync(string sportscenterName)
    {
        const string api = $"https://booking-tpsc.sporetrofit.com/Home/loadLocationPeopleNum";

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(api)
        };
        request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

        using var client = httpClientFactory.CreateClient(); 
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception(response.StatusCode.ToString());
        }

        var body = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<SportscenterPeopleCountInfo>(body);
        
        return result?.LocationPeopleCount?.FirstOrDefault(location => location.LidName == sportscenterName);
    }
}