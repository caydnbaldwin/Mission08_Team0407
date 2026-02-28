using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission8Assignment.Models;
using System.Diagnostics;

namespace Mission8Assignment.Controllers
{
    public class HomeController : Controller
    {
        private IToDoRepository _repo;

        public HomeController(IToDoRepository temp) => _repo = temp;

        // quadrant view
        [HttpGet]
        public IActionResult Index()
        {
            // grab all incomplete tasks
            ViewBag.Tasks = _repo.Tasks
                .Where(task => task.Completed == false)
                .OrderBy(task => task.TaskId)
                .ToList();

            return View();
        }

        // get page to add task
        [HttpGet]
        public IActionResult Add()
        {
            // grab all categories
            ViewBag.Categories = new SelectList(
                _repo.Categories.OrderBy(category => category.CategoryId),
                nameof(Category.CategoryId),
                nameof(Category.CategoryName)
            );

            // show empty form
            return View("Task", new TaskModel());
        }

        // add task
        [HttpPost]
        public IActionResult Add(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                // add task
                _repo.AddTask(task);

                return RedirectToAction("Index");
            }
            else
            {
                // grab all categories again
                ViewBag.Categories = new SelectList(
                    _repo.Categories.OrderBy(c => c.CategoryId),
                    nameof(Category.CategoryId),
                    nameof(Category.CategoryName)
                );

                // show hydrated form
                return View("Task", task);
            }
        }

        // get specific task page
        [HttpGet]
        public IActionResult Task(int id)
        {
            // grab specific task
            TaskModel task = _repo.Tasks
                .Single(task => task.TaskId == id);

            // grab all categories
            ViewBag.Categories = new SelectList(
                _repo.Categories.OrderBy(c => c.CategoryId),
                nameof(Category.CategoryId),
                nameof(Category.CategoryName)
            );

            // show hydrated form with specific task data
            return View(task);
        }

        // edit specific task
        [HttpPost]
        public IActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                // update task
                _repo.UpdateTask(task);

                return RedirectToAction("Index");
            }
            else
            {
                // grab all categories again
                ViewBag.Categories = new SelectList(
                    _repo.Categories.OrderBy(c => c.CategoryId),
                    nameof(Category.CategoryId),
                    nameof(Category.CategoryName)
                );

                //show hydrated form again
                return View(task);
            }
        }

        // get delete specific task confirmation page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // grab specific task
            TaskModel task = _repo.Tasks
                .Single(task => task.TaskId == id);

            return View(task);
        }

        // delete specific task
        [HttpPost]
        public IActionResult Delete(TaskModel task)
        {
            // delete task
            _repo.DeleteTask(task);

            return RedirectToAction("Index");
        }
    }
}
