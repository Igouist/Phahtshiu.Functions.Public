using MediatR;
using Phahtshiu.Functions.Application.Contracts.Services;

namespace Phahtshiu.Functions.Application.UseCases.LineBot;

/// <summary>
/// 處理不支援的訊息
/// </summary>
public record ProcessUnsupportedMessageCommand(string ReplyToken) : IRequest;

public class ProcessUnsupportedMessageCommandHandler(ILineBotService lineBotService) 
    : IRequestHandler<ProcessUnsupportedMessageCommand>
{
    public async Task Handle(ProcessUnsupportedMessageCommand request, CancellationToken cancellationToken)
    {
        const string message = "不支援的指令 = =";
        await lineBotService.ReplyMessageAsync(request.ReplyToken, message);
    }
}
