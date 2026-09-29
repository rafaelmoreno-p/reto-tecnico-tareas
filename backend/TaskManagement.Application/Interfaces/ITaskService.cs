using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetTasksAsync(
        string? status,
        string? priority);

    Task<TaskDto?> GetTaskByIdAsync(int id);
}