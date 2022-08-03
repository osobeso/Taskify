namespace Taskify.AzureTables.Entities
{
    using Azure;
    using Azure.Data.Tables;

    internal class TaskEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = string.Empty;
        public string RowKey { get; set; } = string.Empty;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
