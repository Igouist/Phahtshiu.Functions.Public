using MediatR;
using Phahtshiu.Functions.Applicaiton.RandomNumbers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

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
        
        var result = message switch
        {
            _ when message.StartsWith("/r") => RunRandomNumberCommand(message),
            _ => "請輸入 /r 以取得隨機數"
        };
        
        return result;
    }
    
    private string RunRandomNumberCommand(string message)
    {
        var command = new RandomNumberCommand(message);
        var randomNumber = _mediator.Send(command).Result;
        return randomNumber;
    }
}