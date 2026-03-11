using MediatR;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;

namespace Phahtshiu.Functions.Application.UseCases.Reminder;

/// <summary>
/// 發送每日天氣預報通知
/// </summary>
public record SendWeatherForecastReminderCommand(string LocationName) : IRequest;

public class SendWeatherForecastReminderCommandHandler(
    IWeatherService weatherService,
    INotificationService notificationService)
    : IRequestHandler<SendWeatherForecastReminderCommand>
{
    public async Task Handle(
        SendWeatherForecastReminderCommand request,
        CancellationToken cancellationToken)
    {
        var forecast = await weatherService.GetTodayForecastAsync(request.LocationName, cancellationToken);

        if (forecast is null)
        {
            var errorMessage = new NotificationBody
            {
                Title = "[天氣] 今日天氣預報",
                Message = "無法取得天氣資訊，請稍後再試",
                Group = "Weather"
            };
            await notificationService.NotificationAsync(errorMessage);
            return;
        }

        var message = new NotificationBody
        {
            Title = $"[天氣] {forecast.LocationName} 今日天氣預報",
            Message = $"{forecast.WeatherDescription}｜{forecast.MinTemperature}°C ~ {forecast.MaxTemperature}°C｜降雨機率 {forecast.RainProbability}%",
            Group = "Weather",
            Url = "https://www.cwb.gov.tw/V8/C/W/County/index.html"
        };

        await notificationService.NotificationAsync(message);
    }
}
