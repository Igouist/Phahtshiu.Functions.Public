using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Phahtshiu.Functions.Application.Notification.Models;
using Phahtshiu.Functions.Application.Notification.Services;
using Phahtshiu.Functions.Infrastructure.Bark.Models;
using Phahtshiu.Functions.Infrastructure.Bark.Options;

namespace Phahtshiu.Functions.Infrastructure.Bark;

public class BarkNotificationService : INotificationService
{
    private const int BarkSuccessCode = 200;
    
    private readonly BarkOption _barkOptions;
    private readonly HttpClient _httpClient;

    public BarkNotificationService(
        IOptions<BarkOption> barkOptions,
        IHttpClientFactory httpClientFactory)
    {
        _barkOptions = barkOptions.Value;
        _httpClient = httpClientFactory.CreateClient();
    }

    /// <summary>
    /// 發送通知
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public async Task NotificationAsync(NotificationBody notification)
    {
        var key = _barkOptions.Key;
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key));
        }
        
        // POST
        var url = $"https://api.day.app/{_barkOptions.Key}";
        var body = CreateBarkNotificationBody(notification);
        var json = JsonSerializer.Serialize(body);
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(url),
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode is false)
        {
            throw new Exception("Bark 發送失敗！");
        }
        
        var result = await response.Content.ReadFromJsonAsync<BarkNotificationResponse>();
        if (result?.Code != BarkSuccessCode)
        {
            throw new Exception("Bark 發送失敗！");
        }
    }
    
    private static BarkNotificationBody CreateBarkNotificationBody(NotificationBody notification)
    {
        var result = new BarkNotificationBody
        {
            Title = notification.Title,
            Body = notification.Message
        };
        
        if (string.IsNullOrWhiteSpace(notification.Group) is false)
        {
            result.Group = notification.Group;
        }
        
        if (string.IsNullOrWhiteSpace(notification.Url) is false)
        {
            result.Url = notification.Url;
        }
        
        return result;
    }
}