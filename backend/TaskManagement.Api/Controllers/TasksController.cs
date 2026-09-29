using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks(
        [FromQuery] string? status,
        [FromQuery] string? priority)
    {
        var tasks =
            await _service.GetTasksAsync(
                status,
                priority);

        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task =
            await _service.GetTaskByIdAsync(id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }
}