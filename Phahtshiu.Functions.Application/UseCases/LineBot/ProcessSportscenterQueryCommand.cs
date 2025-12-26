using MediatR;
using Phahtshiu.Functions.Application.Contracts.Services;

namespace Phahtshiu.Functions.Application.UseCases.LineBot;

/// <summary>
/// 處理 LineBot 運動中心查詢指令
/// </summary>
public record ProcessSportscenterQueryCommand(string ReplyToken, string Message) : IRequest;

public class ProcessSportscenterQueryCommandHandler(
    ILineBotService lineBotService,
    ISportscenterService sportscenterService) : IRequestHandler<ProcessSportscenterQueryCommand>
{
    private const string DefaultSportscenterName = "松山";

    public async Task Handle(
        ProcessSportscenterQueryCommand request,
        CancellationToken cancellationToken)
    {
        // 解析訊息，取得運動中心名稱
        var prompt = request.Message.Split(" ");
        var sportscenterName = prompt.Length is 1
            ? DefaultSportscenterName
            : prompt[1];

        // 查詢運動中心資訊
        var locationInfo = await sportscenterService.FetchPeopleCountAsync(sportscenterName);

        if (locationInfo is null || !int.TryParse(locationInfo.SwimmingPeopleCount, out var swimPeopleNum))
        {
            await lineBotService.ReplyMessageAsync(
                request.ReplyToken, 
                "查詢運動中心資訊失敗，請稍後再試，或檢查運動中心名稱是否正確");
            return;
        }

        // 格式化並回覆
        var message = $"{sportscenterName}運動中心 游泳池 目前人數：{swimPeopleNum}";
        await lineBotService.ReplyMessageAsync(request.ReplyToken, message);
    }
}
