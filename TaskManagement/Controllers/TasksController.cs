using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TaskDto>>> GetTasks([FromQuery] TaskFilterDto filter)
        {
            var userId = GetUserId();
            var result = await _taskService.GetTasksAsync(userId, filter);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(Guid id)
        {
            var userId = GetUserId();
            var task = await _taskService.GetTaskByIdAsync(userId, id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask([FromBody]CreateTaskDto createTaskDto)
        {
            var userId = GetUserId();
            var task = await _taskService.CreateTaskAsync(userId, createTaskDto);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDto>> UpdateTask(Guid id, UpdateTaskDto updateTaskDto)
        {
            try
            {
                var userId = GetUserId();
                var task = await _taskService.UpdateTaskAsync(userId, id, updateTaskDto);
                return Ok(task);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            var userId = GetUserId();
            var result = await _taskService.DeleteTaskAsync(userId, id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
