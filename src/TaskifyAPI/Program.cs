using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Taskify.AzureTables;
using TaskifyAPI.Dtos;
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

app.MapPost("/task", async (CreateNewTaskDto dto, ITaskifyManager manager, ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("post_task");
    try
    {
        var result = await manager.CreateNewTaskAsync(dto);
        return Results.Ok(result);
    }
    catch(Exception ex)
    {
        logger.LogError("Exception: {Message}", ex.Message);
        return Results.Problem();
    }
});

app.MapGet("/tasks", async (ITaskifyManager manager, ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("get_tasks");
    try
    {
        var result = await manager.GetRootTasksAsync();
        return Results.Ok(result);
    } 
    catch (Exception ex)
    {
        logger.LogError("Exception: {Message}", ex.Message);
        return Results.Problem();
    }
});

app.MapGet("/task/{id}", async (Guid id, [FromQuery] Guid? parentId, ITaskifyManager manager, ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("get_tasks");
    try
    {
        var result = await manager.GetTaskDetailsAsync(new TaskKey(id, parentId));
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        logger.LogError("Exception: {Message}", ex.Message);
        return Results.Problem();
    }
});


app.UseSwagger();
app.UseSwaggerUI();

app.Run();