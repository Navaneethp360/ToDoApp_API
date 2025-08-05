using Microsoft.EntityFrameworkCore;
using ToDoAppApi.Models; // Add this so it knows about your models

namespace ToDoAppApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        // This constructor tells EF Core to use options like connection string
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // These represent the tables in your database
        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
