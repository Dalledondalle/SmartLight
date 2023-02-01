using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using SmartLight.Shared;

namespace SmartLight.Server.Service
{
    public class CosmosDB
    {
        private Container container;

        public CosmosDB(CosmosClient dbClient, string databaseName, string containerName)
        {
            container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(DeviceUserBinding item)
        {
            await container.CreateItemAsync<DeviceUserBinding>(item, new PartitionKey(item.ID));
        }

        public async Task DeleteItemAsync(string id)
        {
            await container.DeleteItemAsync<DeviceUserBinding>(id, new PartitionKey(id));
        }

        public async Task<DeviceUserBinding> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<DeviceUserBinding> response = await container.ReadItemAsync<DeviceUserBinding>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdateItemAsync(string id, DeviceUserBinding item)
        {
            await container.UpsertItemAsync<DeviceUserBinding>(item, new PartitionKey(id));
        }

        public async Task<List<DeviceUserBinding>> GetItemsAsync(string queryString)
        {
            var query = container.GetItemQueryIterator<DeviceUserBinding>(new QueryDefinition(queryString));
            List<DeviceUserBinding> results = new List<DeviceUserBinding>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }
            return results;
        }
    }
}
