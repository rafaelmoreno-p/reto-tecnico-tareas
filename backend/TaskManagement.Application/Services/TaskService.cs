using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskDto>> GetTasksAsync(
        string? status,
        string? priority)
    {
        var tasks = await _repository.GetAllAsync(
            status,
            priority);

        return tasks.Select(task => new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            CreatedAt = task.CreatedAt
        });
    }

    public async Task<TaskDto?> GetTaskByIdAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return null;

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            CreatedAt = task.CreatedAt
        };
    }
}