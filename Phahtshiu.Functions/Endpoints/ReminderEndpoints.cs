using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Phahtshiu.Functions.Application.Notification;
using Phahtshiu.Functions.Models;
using Phahtshiu.Functions.Options;
using Phahtshiu.Functions.Shared.Extensions;

namespace Phahtshiu.Functions.Endpoints;

/// <summary>
/// 定時提醒相關 Endpoints
/// </summary>
public class ReminderEndpoints
{
    private const string NotificationGroup = "Reminder";
    
    private readonly IMediator _mediator;
    private readonly ILogger<ReminderEndpoints> _logger;
    private readonly ReminderOption _reminderOption;

    public ReminderEndpoints(
        IMediator mediator,
        ILogger<ReminderEndpoints> logger,
        IOptions<ReminderOption> reminderOption)
    {
        _mediator = mediator;
        _logger = logger;
        _reminderOption = reminderOption.Value;
    }

    /// <summary>
    /// 定時提醒去訂便當
    /// </summary>
    [Function("Bento-Reminder")]
    public async Task BentoReminder(
        // 有空的時候改成從 Configuration 取得時間
        //[TimerTrigger("0 30 9 * * 1-5")] TimerInfo timer)
        [TimerTrigger("*/15 * * * * *")] TimerInfo timer)
    {   
        _logger.LogInformation("[Reminder] 開始發送訂便當提醒");
        
        var bentoUrl = _reminderOption.BentoLink;
        if (bentoUrl.IsNullOrWhiteSpace())
        {
            _logger.LogWarning("[Reminder] 未設定訂便當的連結，取消發送提醒");
            return;
        }
        
        var command = new SendNotificationCommand(
            Title: "[Reminder] 該訂午餐了吧！",
            Message: $"訂便當連結 => 作燴-ZoheyEats: {bentoUrl}",
            Url: bentoUrl,
            Group: NotificationGroup);
        
        await _mediator.Send(command);
        
        _logger.LogInformation("[Reminder] 發送訂便當提醒完成");
    }
}