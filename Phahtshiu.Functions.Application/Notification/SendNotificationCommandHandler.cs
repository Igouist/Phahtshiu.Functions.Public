using MediatR;
using Phahtshiu.Functions.Application.Notification.Models;
using Phahtshiu.Functions.Application.Notification.Services;

namespace Phahtshiu.Functions.Application.Notification;

/// <summary>
/// 發送通知
/// </summary>
public record SendNotificationCommand(
    string Title,
    string Message,
    string? Url = null,
    string? Group = null) : IRequest;

public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand>
{
    private readonly INotificationService _notificationService;

    public SendNotificationCommandHandler(
        INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    public Task Handle(
        SendNotificationCommand request, 
        CancellationToken cancellationToken)
    {
        var message = new NotificationBody
        {
            Title = request.Title,
            Message = request.Message,
            Url = request.Url!,
            Group = request.Group!
        };
        
        return _notificationService.NotificationAsync(message);
    }
}