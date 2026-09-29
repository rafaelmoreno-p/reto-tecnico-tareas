using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync(
        string? status,
        string? priority);

    Task<TaskItem?> GetByIdAsync(int id);
}