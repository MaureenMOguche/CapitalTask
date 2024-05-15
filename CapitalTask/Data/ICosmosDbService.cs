namespace CapitalTask.Data;

public interface ICosmosDbService
{
    Task<T> AddItemAsync<T>(T item, string itemId);
    Task DeleteItemAsync<T>(string id);
    Task<T> GetItemAsync<T>(string id);
    Task<IEnumerable<T>> GetItemsAsync<T>(string queryString);
    Task UpdateItemAsync<T>(string id, T item);
}
