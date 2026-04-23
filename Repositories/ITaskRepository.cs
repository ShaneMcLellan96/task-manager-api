using TaskManager.Models;

namespace TaskManager.Repositories;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(string id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task UpdateAsync(string id, TaskItem task);
    Task DeleteAsync(string id);
}
