using Microsoft.AspNetCore.Mvc;
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
        };
    }
}
