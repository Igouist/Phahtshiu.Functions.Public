using Azure.Data.Tables;

namespace Phahtshiu.Functions.Infrastructure.Data;

/// <summary>
/// TableClient 工廠
/// </summary>
public interface ITableClientFactory
{
    /// <summary>
    /// 取得 TableClient
    /// </summary>
    /// <returns></returns>
    TableClient GetTableClient(string tableName);
}

public class TableClientFactory : ITableClientFactory
{
    private static TableServiceClient _serviceClient = null!;

    public TableClientFactory(string connectionString)
    {
        _serviceClient = new TableServiceClient(connectionString);
    }

    public TableClient GetTableClient(string tableName)
    {
        return _serviceClient.GetTableClient(tableName);
    }
}