using MediatR;
using Phahtshiu.Functions.Application.Notification.Models;
using Phahtshiu.Functions.Application.Notification.Services;

namespace Phahtshiu.Functions.Application.Reminder;

/// <summary>
/// 發送早餐提醒
/// </summary>
public record SendBreakfastReminderCommand : IRequest;

public class SendBreakfastReminderCommandHandler : IRequestHandler<SendBreakfastReminderCommand>
{
    private readonly INotificationService _notificationService;

    public SendBreakfastReminderCommandHandler(
        INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    public Task Handle(
        SendBreakfastReminderCommand request, 
        CancellationToken cancellationToken)
    {
        var message = new NotificationBody
        {
            Title = "[Reminder] 該買早餐了吧！",
            Message = "再晚就沒東西吃啦！",
            Group = "Reminder"
        };
        
        return _notificationService.NotificationAsync(message);
    }
}
