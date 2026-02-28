using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mission8Assignment.Models;

namespace Mission8Assignment.Controllers
{
    // Handles all task management actions: listing by quadrant, adding, editing, and deleting tasks
    public class HomeController : Controller
    {
        // Repository injected by the DI container registered in Program.cs
        private IToDoRepository _repo;

        public HomeController(IToDoRepository temp) => _repo = temp;

        // Loads the quadrant view with all incomplete tasks
        [HttpGet]
        public IActionResult Index()
        {
            // Only display tasks that have not been marked as completed
            ViewBag.Tasks = _repo.Tasks
                .Where(task => task.Completed == false)
                .OrderBy(task => task.TaskId)
                .ToList();

            return View();
        }

        // Loads the blank Add Task form with the category dropdown populated
        [HttpGet]
        public IActionResult Add()
        {
            // Populate the category dropdown before rendering the empty form
            ViewBag.Categories = new SelectList(
                _repo.Categories.OrderBy(category => category.CategoryId),
                nameof(Category.CategoryId),
                nameof(Category.CategoryName)
            );

            // Pass an empty TaskModel so the view's asp-for bindings have something to work with
            return View("Task", new TaskModel());
        }

        // Receives the submitted Add Task form and saves the new task to the database
        [HttpPost]
        public IActionResult Add(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                // Save the new task and return to the quadrant view
                _repo.AddTask(task);

                return RedirectToAction("Index");
            }
            else
            {
                // ViewBag is lost on postback, so repopulate the category dropdown before re-rendering
                ViewBag.Categories = new SelectList(
                    _repo.Categories.OrderBy(c => c.CategoryId),
                    nameof(Category.CategoryId),
                    nameof(Category.CategoryName)
                );

                // Return the form with validation errors and the user's entered data intact
                return View("Task", task);
            }
        }

        // Loads the Edit Task form pre-filled with the existing task data
        [HttpGet]
        public IActionResult Task(int id)
        {
            // Use SingleOrDefault to avoid crashing if an invalid ID is passed in the URL
            TaskModel? task = _repo.Tasks.SingleOrDefault(t => t.TaskId == id);
            if (task == null) return RedirectToAction("Index");

            // Populate the category dropdown before rendering the form
            ViewBag.Categories = new SelectList(
                _repo.Categories.OrderBy(c => c.CategoryId),
                nameof(Category.CategoryId),
                nameof(Category.CategoryName)
            );

            // Pass the existing task data so the form fields are pre-filled
            return View(task);
        }

        // Receives the submitted Edit Task form and updates the task in the database
        [HttpPost]
        public IActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                // Save the updated task and return to the quadrant view
                _repo.UpdateTask(task);

                return RedirectToAction("Index");
            }
            else
            {
                // ViewBag is lost on postback, so repopulate the category dropdown before re-rendering
                ViewBag.Categories = new SelectList(
                    _repo.Categories.OrderBy(c => c.CategoryId),
                    nameof(Category.CategoryId),
                    nameof(Category.CategoryName)
                );

                // Return the form with validation errors and the user's entered data intact
                return View("Task", task);
            }
        }

        // Loads the Delete confirmation page with the task's details
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Include Category so the confirmation page can display the category name
            // Use SingleOrDefault to avoid crashing if the task has already been deleted
            TaskModel? task = _repo.Tasks
                .Include(t => t.Category)
                .SingleOrDefault(t => t.TaskId == id);
            if (task == null) return RedirectToAction("Index");

            return View(task);
        }

        // Removes the task from the database after the user confirms deletion
        [HttpPost]
        public IActionResult Delete(TaskModel task)
        {
            // Delete the task record and return to the quadrant view
            _repo.DeleteTask(task);

            return RedirectToAction("Index");
        }
    }
}
