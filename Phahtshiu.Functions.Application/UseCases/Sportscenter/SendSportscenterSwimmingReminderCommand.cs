using MediatR;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;

namespace Phahtshiu.Functions.Application.UseCases.Sportscenter;

/// <summary>
/// 發送運動中心游泳池人數提醒
/// </summary>
public record SendSportscenterSwimmingReminderCommand(string SportscenterName) : IRequest;

public class SendSportscenterSwimmingReminderCommandHandler 
    : IRequestHandler<SendSportscenterSwimmingReminderCommand>
{
    private readonly ISportscenterService _sportscenterService;
    private readonly INotificationService _notificationService;

    public SendSportscenterSwimmingReminderCommandHandler(
        ISportscenterService sportscenterService,
        INotificationService notificationService)
    {
        _sportscenterService = sportscenterService;
        _notificationService = notificationService;
    }
    
    public async Task Handle(
        SendSportscenterSwimmingReminderCommand request, 
        CancellationToken cancellationToken)
    {
        // 查詢指定運動中心的游泳池人數
        var locationInfo = await _sportscenterService
            .FetchPeopleCountAsync(request.SportscenterName);
        
        if (locationInfo is null)
        {
            // 查無資料時的處理
            var errorMessage = new NotificationBody
            {
                Title = $"[運動中心] {request.SportscenterName}運動中心",
                Message = "查詢失敗，請檢查運動中心名稱是否正確",
                Group = "Sportscenter"
            };
            await _notificationService.NotificationAsync(errorMessage);
            return;
        }
        
        // 組合通知訊息
        var message = new NotificationBody
        {
            Title = $"[運動中心] {request.SportscenterName}運動中心游泳池人數",
            Message = $"目前人數：{locationInfo.SwimmingPeopleCount}/{locationInfo.SwimmingMaxPeopleCount}",
            Group = "Sportscenter"
        };
        
        // 發送通知
        await _notificationService.NotificationAsync(message);
    }
}
