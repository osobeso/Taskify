namespace Taskify.AzureTables
{
    using Azure.Data.Tables;
    public interface IAzureTableStorageService
    {
        public TableServiceClient GetClient();
    }
}
