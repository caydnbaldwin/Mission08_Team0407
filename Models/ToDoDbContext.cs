using Microsoft.EntityFrameworkCore;

namespace Mission8Assignment.Models
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed the Category table (required by the dropdown)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Home" },
                new Category { CategoryId = 2, CategoryName = "School" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Church" }
            );

            // Seed some sample tasks spread across all four quadrants
            modelBuilder.Entity<TaskModel>().HasData(
                new TaskModel
                {
                    TaskId = 1,
                    TaskName = "Pay rent",
                    DueDate = new DateOnly(2026, 3, 1),
                    Quadrant = 1,
                    CategoryId = 1,
                    Completed = false
                },
                new TaskModel
                {
                    TaskId = 2,
                    TaskName = "Study for IS 413 exam",
                    DueDate = new DateOnly(2026, 3, 5),
                    Quadrant = 1,
                    CategoryId = 2,
                    Completed = false
                },
                new TaskModel
                {
                    TaskId = 3,
                    TaskName = "Read scriptures daily",
                    DueDate = null,
                    Quadrant = 2,
                    CategoryId = 4,
                    Completed = false
                },
                new TaskModel
                {
                    TaskId = 4,
                    TaskName = "Exercise weekly",
                    DueDate = null,
                    Quadrant = 2,
                    CategoryId = 1,
                    Completed = false
                },
                new TaskModel
                {
                    TaskId = 5,
                    TaskName = "Reply to non-urgent emails",
                    DueDate = null,
                    Quadrant = 3,
                    CategoryId = 3,
                    Completed = false
                },
                new TaskModel
                {
                    TaskId = 6,
                    TaskName = "Reorganize desk",
                    DueDate = null,
                    Quadrant = 4,
                    CategoryId = 1,
                    Completed = false
                }
            );
        }
    }
}
