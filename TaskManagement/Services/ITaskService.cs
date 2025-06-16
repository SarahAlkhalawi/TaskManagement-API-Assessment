using TaskManagement.DTOs;

namespace TaskManagement.Services
{
    public interface ITaskService
    {
        Task<TaskDto> CreateTaskAsync(Guid userId, CreateTaskDto createTaskDto);
        Task<TaskDto> UpdateTaskAsync(Guid userId, Guid taskId, UpdateTaskDto updateTaskDto);
        Task<bool> DeleteTaskAsync(Guid userId, Guid taskId);
        Task<TaskDto> GetTaskByIdAsync(Guid userId, Guid taskId);
        Task<PagedResult<TaskDto>> GetTasksAsync(Guid userId, TaskFilterDto filter);
    }
}
