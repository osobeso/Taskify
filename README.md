# Taskify
- Taskify is a Mock project that aims to give a brief overview on how Minimal API's work on dotnet 6.

## Steps:

### Step 1 - Add Swagger Services
- First step is to add Swagger, it can be done by adding the following lines of code to the `Program.cs` file:
    ```csharp
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "TasksAPI", Version = "v1" });
    });
    ```

- Then we need to Use Swagger and Swagger UI, which can be done by adding these two lines before `app.Run()` in `Program.cs`
    ```csharp
    app.UseSwagger();
    app.UseSwaggerUI();
    ```

- Finally, we configure `Properties/LaunchSettings.json` file to point to the Swagger UI index.
    ```json
        "launchUrl": "swagger/index.html"
    ```

### Step 2 - Configure Azure Storage Tables and Dependencies
- In order to be able to use Azure Storage Tables, we have created a method which allowsus to configure this on the ServiceCollection
  We only need to add the following lines of code:
  ```csharp
    builder.Services.UseAzureStorage((config) =>
    {
        config.UseConnectionString(builder.Configuration.GetConnectionString("AzureTablesConnectionString"));
    });
  ```
- Setup an Environment Variable in your Visual Studio debug configuration, in order to add the Azurite 
connection string for local development:
    ```csharp
    ConnectionStrings__AzureTablesConnectionString='UseDevelopmentStorage=true'
    ```

- Add TaskifyManager as part of the service collection so we can access that dependency in our API's
    ```csharp
    builder.Services.AddScoped<ITaskifyManager, TaskifyManager>();
    ```

### Step 3 - Add Mappings for Sample Methods
- Add the following sample mappings so that we can start calling our test services.
    ```csharp
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
    ```

### Step 4 - Azurite and Azure Storage Explorer
- For this steps, we will install and start Azurite, to be able to use Storage Tables locally,
and we will also setup Azure Storage Explorer, to have visibility on these local tables.

1. Install Azurite
    ```bash
    npm i azurite
    ```
2. Start Azurite
    ```bash
    azurite
    ```
3. Install Azure Storage Explorer
    Link can be found here: https://azure.microsoft.com/en-us/products/storage/storage-explorer/#overview

### Step 5 - Test your API
- We're ready to start testing our API, and see the changes on the Azure Data Explorer.