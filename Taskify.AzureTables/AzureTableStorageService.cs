namespace Taskify.AzureTables
{
    using Azure.Data.Tables;
    internal class AzureTableStorageService : IAzureTableStorageService
    {
        private readonly TableServiceClient TableServiceClient;
        public AzureTableStorageService(AzureTableStorageConfiguration config)
        {
            TableServiceClient = new TableServiceClient(config.ConnectionString);
        }
        public TableServiceClient GetClient()
        {
            return TableServiceClient;
        }
    }
}
