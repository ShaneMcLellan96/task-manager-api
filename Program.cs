using TaskManager.Repositories;
using TaskManager.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowTaskManagerUi", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "https://task-manager-ui-amber.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowTaskManagerUi");
app.UseAuthorization();
app.MapControllers();

app.Run();