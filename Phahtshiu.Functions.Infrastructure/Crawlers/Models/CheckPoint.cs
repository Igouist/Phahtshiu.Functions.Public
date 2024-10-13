using Azure;
using Azure.Data.Tables;

namespace Phahtshiu.Functions.Infrastructure.Crawlers.Models;

/// <summary>
/// 檢查點
/// </summary>
public class CheckPoint : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    
    public DateTimeOffset? LastCheckPoint { get; set; }
}