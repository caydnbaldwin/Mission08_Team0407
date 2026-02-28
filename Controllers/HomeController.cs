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

        [HttpGet]
        public IActionResult Index() 
        {
            ViewBag.Tasks = _repo.Tasks
                .Where(task => task.Completed == false)
                .OrderBy(task => task.TaskId)
                .ToList();

            return View();
        }

        [HttpGet]
        public IActionResult Task()
        {
            ViewBag.Categories = new SelectList(
                _repo.Categories.OrderBy(category => category.CategoryId)
            );

            return View(new TaskModel());
        }

        [HttpPost]
        public IActionResult Task(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _repo.AddTask(task);
            }

            return RedirectToAction()
        }
    }
}
