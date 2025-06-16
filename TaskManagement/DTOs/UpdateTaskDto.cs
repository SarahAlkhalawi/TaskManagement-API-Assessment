using System.ComponentModel.DataAnnotations;
using TaskManagement.Models;

namespace TaskManagement.DTOs
{
    public class UpdateTaskDto
    {
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public TaskPriority? Priority { get; set; }
        public Status? Status { get; set; }
    }
}
