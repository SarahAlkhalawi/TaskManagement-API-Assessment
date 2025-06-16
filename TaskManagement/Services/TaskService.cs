using Microsoft.EntityFrameworkCore;
using TaskManagement.Context;
using TaskManagement.DTOs;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskDto> CreateTaskAsync(Guid userId, CreateTaskDto createTaskDto)
        {
            var task = new TaskItem
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Priority = createTaskDto.Priority,
                Status = createTaskDto.Status,
                UserId = userId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return MapToTaskDto(task);
        }

        public async Task<TaskDto> UpdateTaskAsync(Guid userId, Guid taskId, UpdateTaskDto updateTaskDto)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                throw new ArgumentException("Task not found");

            if (!string.IsNullOrEmpty(updateTaskDto.Title))
                task.Title = updateTaskDto.Title;

            if (updateTaskDto.Description != null)
                task.Description = updateTaskDto.Description;

            if (updateTaskDto.Priority.HasValue)
                task.Priority = updateTaskDto.Priority.Value;

            if (updateTaskDto.Status.HasValue)
                task.Status = updateTaskDto.Status.Value;

            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToTaskDto(task);
        }

        public async Task<bool> DeleteTaskAsync(Guid userId, Guid taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TaskDto> GetTaskByIdAsync(Guid userId, Guid taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            return task != null ? MapToTaskDto(task) : null;
        }

        public async Task<PagedResult<TaskDto>> GetTasksAsync(Guid userId, TaskFilterDto filter)
        {
            var query = _context.Tasks.Where(t => t.UserId == userId).AsQueryable();

            if (filter.Priority.HasValue)
                query = query.Where(t => t.Priority == filter.Priority.Value);

            if (filter.Status.HasValue)
                query = query.Where(t => t.Status == filter.Status.Value);

            if (!string.IsNullOrEmpty(filter.Search))
            {
                var searchTerm = filter.Search.ToLower();
                query = query.Where(t => t.Title.ToLower().Contains(searchTerm) ||
                                        t.Description.ToLower().Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();

            var tasks = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((filter.Page.Value - 1) * filter.PageSize.Value)
                .Take(filter.PageSize.Value)
                .ToListAsync();

            return new PagedResult<TaskDto>
            {
                Items = tasks.Select(MapToTaskDto).ToList(),
                TotalCount = totalCount,
                Page = filter.Page!.Value,
                PageSize = filter.PageSize!.Value,
                TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize.Value)
            };
        }

        private TaskDto MapToTaskDto(TaskItem task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                UserId = task.UserId
            };
        }
    }
}