namespace Mission8Assignment.Models
{
    public interface IToDoRepository
    {
        IQueryable<TaskModel> Tasks { get; }
        IQueryable<Category> Categories { get; }

        void AddTask(TaskModel task);
        void UpdateTask(TaskModel task);
        void DeleteTask(TaskModel task);
    }
}
