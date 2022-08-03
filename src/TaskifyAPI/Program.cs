using Microsoft.OpenApi.Models;
using Taskify.AzureTables;
using TaskifyAPI.Managers;

var builder = WebApplication.CreateBuilder(args);


// Add Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "TasksAPI", Version = "v1" });
});

// Add Azure storage configuration.
builder.Services.UseAzureStorage((config) =>
{
    config.UseConnectionString(builder.Configuration.GetConnectionString("AzureTablesConnectionString"));
});

// Add our Manager.
// NOTE: Scoped, represents the lifespan of our manager.
// There can be Scoped, Transient, and Singleton lifespan for dependencies.
// More info: https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage
builder.Services.AddScoped<ITaskifyManager, TaskifyManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.UseSwagger();
app.UseSwaggerUI();

app.Run();