using EmployeeMvcWithSecuirty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace EmployeeMvcWithSecuirty.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(string pId,string messgeId) {
            ViewBag.pId =  pId;
            ViewBag.message = TempData["msg"] as string;
            ViewBag.msgId = messgeId;
            return View();
        }
        public IActionResult AddTeam()
        {
            return RedirectToAction("Create","Teams");
        }
        public IActionResult ViewTeam()
        {
            return RedirectToAction("Index", "Teams");
        }
        public IActionResult AddPlayer()
        {
            return RedirectToAction("Create", "Players");
        }
        public IActionResult ViewPlayers()
        {
            return RedirectToAction("Index", "Players");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
