using Microsoft.EntityFrameworkCore;
using TodoList.WebApi.Models;
namespace TodoList.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<ToDoListAssignment> ToDoListAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoListAssignment>()
                .HasData(new ToDoListAssignment { Id = 1, Description = "This is example ToDo task", CreatedDate= new DateTime(2025, 3, 11, 0, 0, 0), CompletionStatus = false});
        }
    }
}
