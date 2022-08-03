namespace Taskify.AzureTables
{
    using Microsoft.Extensions.DependencyInjection;
    using Taskify.AzureTables.Repositories;
    using Taskify.Data.Repositories;
    public static class Extensions
    {
        public static IServiceCollection UseAzureStorage(this IServiceCollection collection, Action<AzureTableStorageConfiguration> configure)
        {
            var config = new AzureTableStorageConfiguration();
            configure(config);
            var service = new AzureTableStorageService(config);
            collection.AddSingleton<IAzureTableStorageService>(service);
            collection.AddScoped<ITaskRepository, TaskStorageRepository>();
            TaskStorageRepository.CreateTableIfNotExists(service);
            return collection;
        }
    }
}
