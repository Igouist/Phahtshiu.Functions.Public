using System.Net;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Options;
using Phahtshiu.Functions.Application.UseCases.LineBot;
using Phahtshiu.Functions.Options;
using Phahtshiu.Functions.Shared.Extensions;
using LineBot = isRock.LineBot;

namespace Phahtshiu.Functions.Endpoints;

public class LineBotEndpoints(
    IMediator mediator,
    IOptions<LineBotOption> lineBotOptions)
{
    private readonly LineBotOption _lineBotOptions = lineBotOptions.Value;

    /// <summary>
    /// Line Bot 回覆訊息
    /// </summary>
    /// <param name="req"></param>
    /// <param name="executionContext"></param>
    /// <returns></returns>
    /// <example>https://github.com/isdaviddong/LineBotSdkDotNetCoreWebExample/blob/master/main/main/Controllers/LineBotController.cs</example>
    [Function("Line-Bot-Reply")]
    public async Task<HttpResponseData> LineBotReply(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
        HttpRequestData req,
        FunctionContext executionContext)
    {
        try
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var receivedMessage = isRock.LineBot.Utility.Parsing(body);
        
            var lineEvent = receivedMessage.events.FirstOrDefault()!;
            var lineEventType = lineEvent.type.ToLower();
            var lineMessageType = lineEvent.message.type.ToLower();
            
            if (lineEventType is not "message" || lineMessageType is not "text")
            {
                var unsupportedMessageCommand = new ProcessUnsupportedMessageCommand(lineEvent.replyToken);
                await mediator.Send(unsupportedMessageCommand);
                return req.CreateResponse(HttpStatusCode.OK);
            }
        
            var replyToken = lineEvent.replyToken;
            var message = lineEvent.message.text.ToLower();
            await RouteToCommand(replyToken, message);
            
            return req.CreateResponse(HttpStatusCode.OK);
        }
        catch (Exception exception)
        {
            var errorNotifyCommand = new NotifyLineBotErrorCommand(exception.Message);
            await mediator.Send(errorNotifyCommand);
            throw;
        }
    }

    /// <summary>
    /// 根據訊息內容路由到對應的 Command
    /// </summary>
    private Task RouteToCommand(string replyToken, string message)
    {
        return message switch
        {
            _ when message.StartsWith("/r") => 
                mediator.Send(new ProcessRandomNumberCommand(replyToken, message)),
            _ when message.StartsWith("/swim") => 
                mediator.Send(new ProcessSportscenterQueryCommand(replyToken, message)),
            _ => 
                mediator.Send(new ProcessUnsupportedMessageCommand(replyToken))
        };
    }
}