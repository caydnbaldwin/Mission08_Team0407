using System.ComponentModel.DataAnnotations;

namespace Mission8Assignment.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; } = string.Empty;

        public DateOnly? DueDate { get; set; }

        [Required]
        public int Quadrant { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public bool Completed { get; set; }
    }
}
