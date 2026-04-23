using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _repository;

    public TasksController(ITaskRepository repository) =>
        _repository = repository;

    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAll() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TaskItem>> GetById(string id)
    {
        var task = await _repository.GetByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {
        task.Id = null;
        task.CreatedAt = DateTime.UtcNow;
        var created = await _repository.CreateAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, TaskItem task)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return NotFound();

        task.Id = id;
        task.CreatedAt = existing.CreatedAt;
        await _repository.UpdateAsync(id, task);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
