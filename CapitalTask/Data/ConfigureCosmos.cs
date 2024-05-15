using Microsoft.Azure.Cosmos;

namespace CapitalTask.Data;

/// <summary>
/// Cosmos Db configuration class
/// </summary>
public static class ConfigureCosmos
{
    /// <summary>
    /// Initializes cosmos db with the appropriate settings from config
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    public static async Task<CosmosDbService> Initialize(IConfiguration config)
    {
        var databaseName = config["CosmosDb:DatabaseName"];
        var containerName = config["CosmosDb:ContainerName"];
        var account = config["CosmosDb:Account"];
        var key = config["CosmosDb:Key"];

        var client = new CosmosClient(account, key);
        var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

        var cosmosDbService = new CosmosDbService(client, databaseName, containerName);

        return cosmosDbService;
    }
}
