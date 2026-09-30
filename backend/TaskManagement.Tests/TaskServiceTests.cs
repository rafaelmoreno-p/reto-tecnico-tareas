using Moq;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests;

public class TaskServiceTests
{
    [Fact]
    public async Task GetTaskByIdAsync_ReturnsTask()
    {
        var repository =
            new Mock<ITaskRepository>();

        repository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new TaskItem
            {
                Id = 1,
                Title = "Test",
                Description = "Description",
                Priority = "Alta",
                Status = "Pendiente"
            });

        var service =
            new TaskService(
                repository.Object);

        var result =
            await service.GetTaskByIdAsync(1);

        Assert.NotNull(result);

        Assert.Equal(
            "Test",
            result!.Title);
    }
}