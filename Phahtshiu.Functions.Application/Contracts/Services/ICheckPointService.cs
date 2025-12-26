namespace Phahtshiu.Functions.Application.Contracts.Services;

/// <summary>
/// 檢查點服務
/// </summary>
public interface ICheckPointService
{
    /// <summary>
    /// 取得檢查點時間
    /// </summary>
    /// <returns></returns>
    Task<DateTimeOffset> GetCheckPointAsync(string key, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// 更新檢查點時間
    /// </summary>
    /// <returns></returns>
    Task UpsertCheckPointAsync(string key, DateTimeOffset checkPoint, CancellationToken cancellationToken = default);
}