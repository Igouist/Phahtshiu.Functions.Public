using MediatR;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;

namespace Phahtshiu.Functions.Application.UseCases.Reminder;

/// <summary>
/// 發送早餐提醒
/// </summary>
public record SendBreakfastReminderCommand : IRequest;

public class SendBreakfastReminderCommandHandler(
    INotificationService notificationService) 
    : IRequestHandler<SendBreakfastReminderCommand>
{
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
        
        return notificationService.NotificationAsync(message);
    }
}
