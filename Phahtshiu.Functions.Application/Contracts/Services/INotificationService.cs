using Phahtshiu.Functions.Application.Contracts.Models;

namespace Phahtshiu.Functions.Application.Contracts.Services;

/// <summary>
/// 通知服務
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// 發送通知
    /// </summary>
    /// <returns></returns>
    Task NotificationAsync(NotificationBody notification);
}