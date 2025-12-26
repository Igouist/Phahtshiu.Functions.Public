using Phahtshiu.Functions.Application.Contracts.Models;

namespace Phahtshiu.Functions.Application.Contracts.Services;

/// <summary>
/// 運動中心相關服務
/// </summary>
public interface ISportscenterService
{
    /// <summary>
    /// 取得指定運動中心的人數資訊
    /// </summary>
    /// <param name="sportscenterName">運動中心名稱</param>
    /// <returns>該運動中心的人數資訊，找不到時回傳 null</returns>
    Task<SportscenterLocationPeopleCountInfo?> FetchPeopleCountAsync(string sportscenterName);
}