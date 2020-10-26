namespace ServiceBusBootstrapper.DataAccess
{
    public class CosmosDbOptions
    {
        public string CosmosConnectionString { get; set; }
        public string CosmosDatabaseName { get; set; }
        public string CosmosContainerName { get; set; }
    }
}