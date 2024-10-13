using Azure.Data.Tables;
using Microsoft.Extensions.Logging;
using Phahtshiu.Functions.Application.Crawlers.Services;
using Phahtshiu.Functions.Infrastructure.Crawlers.Models;
using Phahtshiu.Functions.Infrastructure.Data;

namespace Phahtshiu.Functions.Infrastructure.Crawlers;

/// <summary>
/// 檢查點服務
/// </summary>
public class CheckPointService : ICheckPointService
{
    private const string TableName = "CrawlerCheckPoints";
    
    private readonly ILogger<CheckPointService> _logger;
    private readonly TableClient _tableClient;

    public CheckPointService(
        ILogger<CheckPointService> logger,
        ITableClientFactory tableClientFactory)
    {
        _logger = logger;
        _tableClient = tableClientFactory.GetTableClient(TableName);
    }

    /// <summary>
    /// 取得最後一次的檢查點
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DateTimeOffset> GetCheckPointAsync(
        string key, 
        CancellationToken cancellationToken = default)
    {
        var defaultResult = DateTimeOffset.MinValue; // 如果沒有資料，代表從未執行過，回傳最小值
        
        const string partitionKey = "LastCheckPoint"; // 目前沒有分群的需求，可以先都放在同個分區就好
        var rowKey = key;
        var row = await _tableClient.GetEntityAsync<CheckPoint>(
            partitionKey, rowKey, 
            cancellationToken: cancellationToken);

        var response = row.GetRawResponse();
        if (response.IsError is false && 
            row.Value is not null)
        {
            return row.Value.LastCheckPoint ?? defaultResult;
        }
        
        if (response.Status is 404)
        {
            _logger.LogWarning("找不到檢查點資料，回傳最小值");
            return defaultResult;
        }

        _logger.LogError("取得檢查點資料時發生錯誤！將回傳最小值。 Response: {Response}", response);
        return defaultResult;
    }

    /// <summary>
    /// 更新檢查點時間
    /// </summary>
    /// <returns></returns>
    public async Task UpsertCheckPointAsync(
        string key, 
        DateTimeOffset checkPoint, 
        CancellationToken cancellationToken = default)
    {
        const string partitionKey = "LastCheckPoint"; // 目前沒有分群的需求，可以先都放在同個分區就好

        var entity = new CheckPoint
        {
            PartitionKey = partitionKey,
            RowKey = key,
            LastCheckPoint = checkPoint
        };
        
        await _tableClient.UpsertEntityAsync(entity, cancellationToken: cancellationToken);
    }
}