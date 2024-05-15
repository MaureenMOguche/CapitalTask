using Microsoft.Azure.Cosmos;

namespace CapitalTask.Data;

/// <summary>
/// Service for access cosmos db
/// </summary>
public class CosmosDbService : ICosmosDbService
{
    private Container _container;

    /// <summary>
    /// Constructor to receive database name and container name and fetch cosmos container
    /// </summary>
    /// <param name="cosmosClient"></param>
    /// <param name="databaseName"></param>
    /// <param name="containerName"></param>
    public CosmosDbService(CosmosClient cosmosClient,
        string databaseName,
        string containerName)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    /// <summary>
    /// Add item to cosmosdb
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public async Task<T> AddItemAsync<T>(T item, string itemId)
    {
        return await _container.CreateItemAsync(item, new PartitionKey(itemId));
    }

    public async Task DeleteItemAsync<T>(string id)
    {
        await _container.DeleteItemAsync<T>(id, new PartitionKey(id));
    }

    public async Task<T> GetItemAsync<T>(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex)
        {
            Console.WriteLine(ex);
            return default!;
        }
    }

    public async Task<IEnumerable<T>> GetItemsAsync<T>(string queryString)
    {
        var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
        var results = new List<T>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        return results;
    }

    public Task UpdateItemAsync<T>(string id, T item)
    {
        return _container.UpsertItemAsync(item, new PartitionKey(id));
    }
}
