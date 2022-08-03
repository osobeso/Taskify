namespace Taskify.AzureTables
{
    public class AzureTableStorageConfiguration
    {
        internal string ConnectionString = string.Empty;
        public void UseConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
