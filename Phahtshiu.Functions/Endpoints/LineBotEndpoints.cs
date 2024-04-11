using MediatR;
using Phahtshiu.Functions.Application.RandomNumbers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Phahtshiu.Functions.Application.Sportscenter;

namespace Phahtshiu.Functions.Endpoints;

public class LineBotEndpoints
{
    private readonly IMediator _mediator;

    public LineBotEndpoints(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// 測試用 Function
    /// </summary>
    /// <returns></returns>
    [Function("Line-Bot-Testing")]
    public async Task<string> TestingLineBotReply(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        var message = await new StreamReader(req.Body).ReadToEndAsync();
        if (string.IsNullOrEmpty(message))
        {
            return "請輸入訊息= =";
        }
        
        var reply = message switch
        {
            _ when message.StartsWith("/r") => RunRandomNumberCommand(message),
            _ when message.StartsWith("/swim") => FetchSportscenterSwimmingPeopleCountCommand(message),
            _ => DefaultReply()
        };
        
        return await reply;
    }
    
    private static Task<string> DefaultReply()
    {
        return Task.FromResult("請輸入 /r 以取得隨機數");
    }
    
    private async Task<string> RunRandomNumberCommand(string message)
    {
        var command = new RandomNumberCommand(message);
        var randomNumber = await _mediator.Send(command);
        return randomNumber;
    }

    private async Task<string> FetchSportscenterSwimmingPeopleCountCommand(string message)
    {
        var command = new FetchSportscenterSwimmingPeopleCountCommand(message);
        var swimPeopleNum = await _mediator.Send(command);
        return swimPeopleNum;
    }
}