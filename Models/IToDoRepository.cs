namespace Mission8Assignment.Models
{
    // Defines the contract for all database operations — implemented by EFToDoRepository
    public interface IToDoRepository
    {
        // Queryable collection of all tasks — callers can chain .Where(), .OrderBy(), etc.
        IQueryable<TaskModel> Tasks { get; }

        // Queryable collection of all categories — used to populate the category dropdown
        IQueryable<Category> Categories { get; }

        // Adds a new task to the database
        void AddTask(TaskModel task);

        // Updates an existing task in the database
        void UpdateTask(TaskModel task);

        // Removes a task from the database
        void DeleteTask(TaskModel task);
    }
}
