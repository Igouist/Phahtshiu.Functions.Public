using MediatR;
using Microsoft.Azure.Functions.Worker;
using Phahtshiu.Functions.Application.Notification;
using Phahtshiu.Functions.Models;

namespace Phahtshiu.Functions.Endpoints;

/// <summary>
/// 定時提醒相關 Endpoints
/// </summary>
public class ReminderEndpoints
{
    private const string NotificationGroup = "Reminder";
    
    private readonly IMediator _mediator;

    public ReminderEndpoints(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// 定時提醒去訂便當
    /// </summary>
    [Function("Bento-Reminder")]
    public async Task BentoReminder(
        // 有空的時候改成從 Configuration 取得時間
        [TimerTrigger("0 30 9 * * 1-5")] TimerInfo timer)
    {   
        const string bentoUrl = "https://eats.quickclick.cc/togo?p=zoheyeats";
        
        var command = new SendNotificationCommand(
            Title: "[Reminder] 該訂午餐了吧！",
            Message: $"訂便當連結 => 作燴-ZoheyEats: {bentoUrl}",
            Url: bentoUrl,
            Group: NotificationGroup);
        
        await _mediator.Send(command);
    }
}