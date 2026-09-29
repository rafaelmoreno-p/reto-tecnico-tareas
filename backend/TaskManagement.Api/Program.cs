using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString(
        "DefaultConnection");

builder.Services.AddSingleton(
    new SqlConnectionFactory(connectionString!));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MobileApp", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MobileApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();