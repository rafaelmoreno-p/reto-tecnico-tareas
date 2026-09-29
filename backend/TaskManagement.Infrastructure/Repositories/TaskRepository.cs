using System.Data;
using Microsoft.Data.SqlClient;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public TaskRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(
        string? status,
        string? priority)
    {
        var tasks = new List<TaskItem>();

        using var connection =
            _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Tasks_Get",
            connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@Status",
            (object?)status ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@Priority",
            (object?)priority ?? DBNull.Value);

        await connection.OpenAsync();

        using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            tasks.Add(new TaskItem
            {
                Id = reader.GetInt32(
                    reader.GetOrdinal("Id")),

                Title = reader.GetString(
                    reader.GetOrdinal("Title")),

                Description = reader.IsDBNull(
                    reader.GetOrdinal("Description"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("Description")),

                Priority = reader.GetString(
                    reader.GetOrdinal("Priority")),

                Status = reader.GetString(
                    reader.GetOrdinal("Status")),

                CreatedAt = reader.GetDateTime(
                    reader.GetOrdinal("CreatedAt"))
            });
        }

        return tasks;
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_Tasks_GetById",
            connection);

        command.CommandType =
            CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@Id",
            id);

        await connection.OpenAsync();

        using var reader =
            await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return null;

        return new TaskItem
        {
            Id = reader.GetInt32(
                reader.GetOrdinal("Id")),

            Title = reader.GetString(
                reader.GetOrdinal("Title")),

            Description = reader.IsDBNull(
                reader.GetOrdinal("Description"))
                ? null
                : reader.GetString(
                    reader.GetOrdinal("Description")),

            Priority = reader.GetString(
                reader.GetOrdinal("Priority")),

            Status = reader.GetString(
                reader.GetOrdinal("Status")),

            CreatedAt = reader.GetDateTime(
                reader.GetOrdinal("CreatedAt"))
        };
    }
}