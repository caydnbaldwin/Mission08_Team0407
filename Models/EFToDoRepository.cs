namespace Mission8Assignment.Models
{
    // Concrete implementation of IToDoRepository using Entity Framework Core and SQLite
    public class EFToDoRepository : IToDoRepository
    {
        // Database context injected by the DI container registered in Program.cs
        private ToDoDbContext _context;

        // Constructor receives the context from the DI container
        public EFToDoRepository(ToDoDbContext temp)
        {
            _context = temp;
        }

        // Exposes all tasks as a queryable so callers can filter and sort without loading everything
        public IQueryable<TaskModel> Tasks => _context.Tasks;

        // Exposes all categories as a queryable for building dropdowns
        public IQueryable<Category> Categories => _context.Categories;

        // Adds a new task record to the database and saves immediately
        public void AddTask(TaskModel task)
        {
            _context.Add(task);
            _context.SaveChanges();
        }

        // Updates an existing task record in the database and saves immediately
        public void UpdateTask(TaskModel task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }

        // Removes a task record from the database by primary key and saves immediately
        public void DeleteTask(TaskModel task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}
