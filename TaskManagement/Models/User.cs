using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
