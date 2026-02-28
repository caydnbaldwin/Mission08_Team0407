using System.ComponentModel.DataAnnotations;

namespace Mission8Assignment.Models
{
    // Represents a single task on the Covey Time Management Matrix
    public class TaskModel
    {
        // Primary key — auto-incremented by SQLite
        [Key]
        public int TaskId { get; set; }

        // The name/description of the task — required so the form rejects empty submissions
        [Required]
        public string TaskName { get; set; } = string.Empty;

        // Optional due date — nullable because not all tasks have a deadline
        public DateOnly? DueDate { get; set; }

        // Which of the four Covey quadrants this task belongs to (1–4) — required
        [Required]
        [Range(1, 4, ErrorMessage = "Please select a valid quadrant")]
        public int Quadrant { get; set; }

        // Foreign key linking to the Categories lookup table — must match a seeded category
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        // Navigation property — lets us access Category.CategoryName without a manual join
        public Category? Category { get; set; }

        // Tracks whether the task has been marked as done; completed tasks are hidden from the main view
        public bool Completed { get; set; }
    }
}
