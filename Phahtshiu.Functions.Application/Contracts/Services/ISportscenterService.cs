using Phahtshiu.Functions.Application.Contracts.Models;

namespace Phahtshiu.Functions.Application.Contracts.Services;

/// <summary>
/// 運動中心相關服務
/// </summary>
public interface ISportscenterService
{
    /// <summary>
    /// 取得運動中心人數資訊
    /// </summary>
    /// <returns></returns>
    Task<SportscenterPeopleCountInfo> FetchPeopleCountAsync();
}