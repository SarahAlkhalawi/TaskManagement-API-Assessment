using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class TaskFilterDto
    {
        public TaskPriority? Priority { get; set; }
        public Status? Status { get; set; }
        public string? Search { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
