using MediatR;
using Phahtshiu.Functions.Application.Contracts.Models;
using Phahtshiu.Functions.Application.Contracts.Services;

namespace Phahtshiu.Functions.Application.UseCases.LineBot;

/// <summary>
/// 通知 LineBot 錯誤
/// </summary>
public record NotifyLineBotErrorCommand(string ErrorMessage) : IRequest;

public class NotifyLineBotErrorCommandHandler(INotificationService notificationService) 
    : IRequestHandler<NotifyLineBotErrorCommand>
{
    public async Task Handle(NotifyLineBotErrorCommand request, CancellationToken cancellationToken)
    {
        var notification = new NotificationBody
        {
            Title = "LineBot Exception",
            Message = request.ErrorMessage
        };
        
        await notificationService.NotificationAsync(notification);
    }
}
