using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OSK_App.DbContexts;
using OSK_App.Entities;
using OSK_App.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ApplicationContext context;
        private readonly string SessionID = "_Id";

        public HomeController(ApplicationContext context) {
            this.context = context;
        }

        public IActionResult Index() {
            if(HttpContext.Session.GetString("_Id") != null) {
                return RedirectToAction("MainPage", "Panel");
            }
            else {
                return View();
            }
        }

        [Route("Login")]
        public IActionResult Login() {
            if (HttpContext.Session.GetString("_Id") != null) {
                return RedirectToAction("MainPage", "Panel");
            }
            else {
                return View();
            }
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Employee e) {
            var employee = context.employees.Where(q => q.User.UserName == e.User.UserName && q.User.Password == e.User.Password
                && q.Role.ID == 1).FirstOrDefault();

            if(employee != null) {
                //TempData["UserId"] = employee.UserID;
                HttpContext.Session.SetInt32(SessionID, employee.UserID);
                return RedirectToAction("MainPage" ,"Panel");
            }
            else {
                return Content("Bad D:");
            }

        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
