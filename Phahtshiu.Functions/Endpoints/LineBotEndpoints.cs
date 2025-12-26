using System.Net;
using MediatR;
using Phahtshiu.Functions.Application.UseCases.RandomNumbers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Options;
using Phahtshiu.Functions.Application.UseCases.Sportscenter;
using Phahtshiu.Functions.Options;
using Phahtshiu.Functions.Shared.Extensions;
using LineBot = isRock.LineBot;

namespace Phahtshiu.Functions.Endpoints;

public class LineBotEndpoints(
    IMediator mediator,
    IOptions<LineBotOption> lineBotOptions)
{
    private readonly LineBotOption _lineBotOptions = lineBotOptions.Value;

    private Task<string> Run(string message)
    {
        var reply = message switch
        {
            _ when message.StartsWith("/r") => RunRandomNumberCommand(message),
            _ when message.StartsWith("/swim") => FetchSportscenterSwimmingPeopleCountCommand(message),
            _ => DefaultReply()
        };
        return reply;
    }

    /// <summary>
    /// Line Bot 測試用 Function
    /// </summary>
    /// <returns></returns>
    [Function("Line-Bot-Testing")]
    public async Task<string> TestingLineBotReply(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] 
        HttpRequestData req)
    {
        var message = await new StreamReader(req.Body).ReadToEndAsync();
        if (message.IsNullOrEmpty())
        {
            return "請輸入訊息= =";
        }
        
        return await Run(message);
    }

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
        var token = _lineBotOptions.ChannelAccessToken;
        var bot = new LineBot.Bot(token);
        
        try
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var receivedMessage = isRock.LineBot.Utility.Parsing(body);
        
            var lineEvent = receivedMessage.events.FirstOrDefault()!;
            var lineEventType = lineEvent.type.ToLower();
            var lineMessageType = lineEvent.message.type.ToLower();
            if (lineEventType is not "message" || lineMessageType is not "text")
            {
                ReplyMessage(bot, lineEvent, "不支援的格式（你以為我買得起貼圖嗎？）");
                return req.CreateResponse(HttpStatusCode.OK);
            }
        
            var text = lineEvent.message.text.ToLower();
            var reply = await Run(text);
            
            ReplyMessage(bot, lineEvent, reply);
            return req.CreateResponse(HttpStatusCode.OK);
        }
        catch (Exception exception)
        {
            var adminId = _lineBotOptions.UserId;
            bot.PushMessage(adminId, "LineBot Exception : \n" + exception.Message);
            throw;
        }
    }

    private static void ReplyMessage(
        LineBot.Bot bot,
        LineBot.Event lineEvent,
        string context)
    {
        var responseMessages = new List<LineBot.MessageBase>()
        {
            new LineBot.TextMessage(context)
        };
        bot.ReplyMessage(lineEvent.replyToken, responseMessages);
    }

    #region 處理訊息動作
    
    private static Task<string> DefaultReply()
    {
        return Task.FromResult("不支援的指令 = =");
    }
    
    private async Task<string> RunRandomNumberCommand(string message)
    {
        var command = new RandomNumberCommand(message);
        var randomNumber = await mediator.Send(command);
        return randomNumber;
    }

    private async Task<string> FetchSportscenterSwimmingPeopleCountCommand(string message)
    {
        var command = new FetchSportscenterSwimmingPeopleCountCommand(message);
        var swimPeopleNum = await mediator.Send(command);
        return swimPeopleNum;
    }
    
    #endregion
}