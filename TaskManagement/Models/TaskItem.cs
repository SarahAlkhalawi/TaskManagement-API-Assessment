using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public TaskPriority Priority { get; set; }

        [Required]
        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; } 
    }

    public enum TaskPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    public enum Status
    {
        Pending = 0,
        InProgress = 1,
        Completed = 2,
        Cancelled = 3
    }
}
