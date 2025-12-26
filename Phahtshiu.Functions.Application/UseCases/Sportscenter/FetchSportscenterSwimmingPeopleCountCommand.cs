using MediatR;
using Phahtshiu.Functions.Application.Contracts.Services;

namespace Phahtshiu.Functions.Application.UseCases.Sportscenter;

public record FetchSportscenterSwimmingPeopleCountCommand(string Message) : IRequest<string>;

public class FetchSportscenterSwimmingPeopleCountCommandHandler 
    : IRequestHandler<FetchSportscenterSwimmingPeopleCountCommand, string>
{
    private readonly ISportscenterService _sportscenterService;
    private const string DefaultSportscenterName = "松山"; // 目前也只查台北，乾脆預設先抓松山

    public FetchSportscenterSwimmingPeopleCountCommandHandler(
        ISportscenterService sportscenterService)
    {
        _sportscenterService = sportscenterService;
    }

    public async Task<string> Handle(
        FetchSportscenterSwimmingPeopleCountCommand request, 
        CancellationToken cancellationToken)
    {
        var prompt = request.Message.Split(" ");
        var sportscenterName = prompt.Length is 1
            ? DefaultSportscenterName
            : prompt[1];

        var locationInfo = await _sportscenterService.FetchPeopleCountAsync(sportscenterName);

        if (locationInfo is null || !int.TryParse(locationInfo.SwimmingPeopleCount, out var swimPeopleNum))
        {
            return "查詢運動中心資訊失敗，請稍後再試，或檢查運動中心名稱是否正確";
        }

        return $"{sportscenterName}運動中心 游泳池 目前人數：{swimPeopleNum}";
    }
}