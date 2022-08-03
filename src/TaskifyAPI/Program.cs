using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "TasksAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.UseSwagger();
app.UseSwaggerUI();

app.Run();