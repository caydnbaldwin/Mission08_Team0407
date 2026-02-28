namespace Mission8Assignment.Models
{
    public class EFToDoRepository : IToDoRepository
    {
        private ToDoDbContext _context;

        public EFToDoRepository(ToDoDbContext temp)
        {
            _context = temp;
        }

        public IQueryable<TaskModel> Tasks => _context.Tasks;
        public IQueryable<Category> Categories => _context.Categories;

        public void AddTask(TaskModel task)
        {
            _context.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskModel task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(TaskModel task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}
