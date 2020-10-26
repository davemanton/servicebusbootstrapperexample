using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;

namespace ServiceBusBootstrapper.DataAccess
{
    public class TopicDataProvider : IDataProvider<Topic>
    {
        private readonly CosmosDbOptions _config;
        private readonly CosmosClient _client;

        public TopicDataProvider(IOptions<CosmosDbOptions> config)
        {
            _config = config.Value;
            _client = new CosmosClient(_config.CosmosConnectionString);
        }

        public async Task<ICollection<Topic>> Read()
        {
            var container = _client.GetContainer(_config.CosmosDatabaseName, _config.CosmosContainerName);

            var queryable = container.GetItemLinqQueryable<Topic>().Where(x => x.Type == "topic").ToFeedIterator();

            var topics = new List<Topic>();

            while(queryable.HasMoreResults)
                topics.AddRange(await queryable.ReadNextAsync());

            return topics;
        }
    }
}