using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Phahtshiu.Functions.Application.Crawlers;
using Phahtshiu.Functions.Application.Notification;
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

    public ReminderEndpoints(
        IMediator mediator,
        ILogger<ReminderEndpoints> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// 定時提醒去買早餐
    /// </summary>
    [Function("Breakfast-Reminder")]
    public async Task BreakfastReminder(
        // 有空的時候改成從 Configuration 取得時間
        [TimerTrigger("0 0 1 * * 1-5")] Microsoft.Azure.Functions.Worker.TimerInfo timer)
    {   
        _logger.LogInformation("[Reminder] 開始發送買早餐提醒");
        
        var command = new SendNotificationCommand(
            Title: "[Reminder] 該買早餐了吧！",
            Message: "再晚就沒東西吃啦！",
            Group: NotificationGroup);
        
        await _mediator.Send(command);
        
        _logger.LogInformation("[Reminder] 發送買早餐提醒完成");
    }
    
    /// <summary>
    /// 定時檢查 Steam 免費遊戲消息
    /// </summary>
    /// <param name="timer"></param>
    [Function("Check-Steam-Free-Game-News")]
    public async Task CheckSteamFreeGameNews(
        [TimerTrigger("0 0 10 * * *")] Microsoft.Azure.Functions.Worker.TimerInfo timer)
    {
        _logger.LogInformation("[Reminder] 開始檢查 Steam 免費遊戲消息");
        
        var command = new CheckSteamFreeGameNewsCommand();
        _ = await _mediator.Send(command);
        
        _logger.LogInformation("[Reminder] 檢查 Steam 免費遊戲消息完成");
    }
    
    /// <summary>
    /// 手動檢查 Steam 免費遊戲消息
    /// </summary>
    /// <param name="req"></param>
    [Function("Check-Steam-Free-Game-News-By-Manual")]
    public async Task<string> CheckSteamFreeGameNewsByManual(
        [HttpTrigger] HttpRequestData req)
    {
        _logger.LogInformation("[Reminder] 開始檢查 Steam 免費遊戲消息");
        
        var command = new CheckSteamFreeGameNewsCommand();
        var message = await _mediator.Send(command);
        
        _logger.LogInformation("[Reminder] 檢查 Steam 免費遊戲消息完成");
        return message;
    }
}