using System.ComponentModel.DataAnnotations;

namespace Mission8Assignment.Models
{
    // Lookup table for task categories — populated via seed data in ToDoDbContext
    public class Category
    {
        // Primary key — auto-incremented by SQLite
        [Key]
        public int CategoryId { get; set; }

        // Display name shown in the category dropdown on the task form
        public string CategoryName { get; set; } = string.Empty;
    }
}
