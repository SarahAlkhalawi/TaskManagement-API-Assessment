using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

    }
}
