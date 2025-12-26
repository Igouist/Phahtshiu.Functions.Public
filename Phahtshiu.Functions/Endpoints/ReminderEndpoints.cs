using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Phahtshiu.Functions.Application.UseCases.Crawlers;
using Phahtshiu.Functions.Application.UseCases.Reminder;
using Phahtshiu.Functions.Application.UseCases.Sportscenter;
using Phahtshiu.Functions.Options;
using Phahtshiu.Functions.Shared.Extensions;

namespace Phahtshiu.Functions.Endpoints;

/// <summary>
/// 定時提醒相關 Endpoints
/// </summary>
public class ReminderEndpoints(
    IMediator mediator,
    ILogger<ReminderEndpoints> logger,
    IOptions<ReminderOption> reminderOption)
{
    private readonly ReminderOption _reminderOption = reminderOption.Value;

    /// <summary>
    /// 定時提醒去買早餐
    /// </summary>
    [Function("Breakfast-Reminder")]
    public async Task BreakfastReminder(
        // 有空的時候改成從 Configuration 取得時間
        [TimerTrigger("0 0 1 * * 1-5")] Microsoft.Azure.Functions.Worker.TimerInfo timer)
    {   
        logger.LogInformation("[Reminder] 開始發送買早餐提醒");
        
        var command = new SendBreakfastReminderCommand();
        await mediator.Send(command);
        
        logger.LogInformation("[Reminder] 發送買早餐提醒完成");
    }
    
    /// <summary>
    /// 定時檢查 Steam 免費遊戲消息
    /// </summary>
    /// <param name="timer"></param>
    [Function("Check-Steam-Free-Game-News")]
    public async Task CheckSteamFreeGameNews(
        [TimerTrigger("0 0 10 * * *")] Microsoft.Azure.Functions.Worker.TimerInfo timer)
    {
        logger.LogInformation("[Reminder] 開始檢查 Steam 免費遊戲消息");
        
        var command = new CheckSteamFreeGameNewsCommand();
        _ = await mediator.Send(command);
        
        logger.LogInformation("[Reminder] 檢查 Steam 免費遊戲消息完成");
    }
    
    /// <summary>
    /// 手動檢查 Steam 免費遊戲消息
    /// </summary>
    /// <param name="req"></param>
    [Function("Check-Steam-Free-Game-News-By-Manual")]
    public async Task<string> CheckSteamFreeGameNewsByManual(
        [HttpTrigger] HttpRequestData req)
    {
        logger.LogInformation("[Reminder] 開始檢查 Steam 免費遊戲消息");
        
        var command = new CheckSteamFreeGameNewsCommand();
        var message = await mediator.Send(command);
        
        logger.LogInformation("[Reminder] 檢查 Steam 免費遊戲消息完成");
        return message;
    }
    
    /// <summary>
    /// 定時查詢運動中心游泳池人數並發送通知
    /// </summary>
    [Function("Sportscenter-Swimming-Pool-Reminder")]
    public async Task SportscenterSwimmingPoolReminder(
        [TimerTrigger("0 0 23 * * *")] Microsoft.Azure.Functions.Worker.TimerInfo timer)
    {
        logger.LogInformation("[Reminder] 開始查詢運動中心游泳池人數");
        
        var command = new SendSportscenterSwimmingReminderCommand(_reminderOption.SportscenterName);
        await mediator.Send(command);
        
        logger.LogInformation("[Reminder] 運動中心游泳池人數查詢完成");
    }
}