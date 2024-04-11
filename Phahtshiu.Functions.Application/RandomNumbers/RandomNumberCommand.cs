using MediatR;

namespace Phahtshiu.Functions.Application.RandomNumbers;

public record RandomNumberCommand(string Message) : IRequest<string>;

/// <summary>
/// 隨機數指令處理器
/// prompt: /r {最大值?} {選項名稱?}
/// ex: "/r", "/r 20", "/r 20 蘋果", "/r 20 蘋果 香蕉"
/// </summary>
public class RandomNumberCommandHandler : IRequestHandler<RandomNumberCommand, string>
{
    public Task<string> Handle(
        RandomNumberCommand request, 
        CancellationToken cancellationToken)
    {
        var max = 100;
        
        // "/r" = 單純擲數的場合
        var prompt = request.Message.Split(" ");
        if (prompt.Length is 1)
        {
            return Task.FromResult($"你骰出了【{GenerateRandomNumber(max)}:{max}】");
        }
        
        // 判斷是否有指定最大值
        var isMaxAssigned = int.TryParse(prompt[1], out var assignedMax);
        max = isMaxAssigned ? assignedMax : max;
        
        // "/r 20" = 只指定最大值的場合
        if (isMaxAssigned && 
            prompt.Length is 2)
        {
            return Task.FromResult($"你骰出了【{GenerateRandomNumber(max)}:{max}】");
        }
        
        // 如果第二個詞用來宣告最大值了，抓選項時就往後跳一個
        var startIndex = isMaxAssigned ? 2 : 1; 
        
        // "/r 蘋果 香蕉" = 多個選項的場合
        // "/r 20 蘋果 香蕉" = 多個選項，且指定最大值的場合
        var results = prompt[startIndex..]
            .Select(word => (word, value: GenerateRandomNumber(max)))
            .OrderByDescending(result => result.value)
            .Select(result =>  $"{result.word}【{result.value}:{max}】");
        
        return Task.FromResult(string.Join(Environment.NewLine, results));
    }
    
    private static int GenerateRandomNumber(int max)
    {
        const int min = 0;
        
        var seed = Guid.NewGuid().GetHashCode();
        var random = new Random(seed).Next(min, max + 1);
        return random;
    }
}