using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSK_App.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Controllers
{
    //[ApiController]
    [Route("/Panel")]
    public class PanelController : Controller
    {
        private readonly ApplicationContext context;
        private int sessionId;

        public PanelController(ApplicationContext context) {
            this.context = context;
        }

        [Route("StronaGlowna")]
        public IActionResult MainPage() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                sessionId = (Int32)HttpContext.Session.GetInt32("_Id");
                TempData["UserId"] = sessionId;
                return View();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        /*[Route("/Kursant")]
        public IActionResult Kursant() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                sessionId = (Int32)HttpContext.Session.GetInt32("_Id");
                TempData["UserId"] = sessionId;
                return RedirectToAction("GetStudents", "Student");
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }*/

        [Route("LogOut")]
        public IActionResult LogOut() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                HttpContext.Session.Remove("_Id");
                return RedirectToAction("Index", "Home");
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [Route("GetNameEmployee")]
        public IActionResult GetNameEmployee(int id) {
            var user = context.users.Where(q => q.ID == id).FirstOrDefault();

            string str = user.FirstName + " " + user.Surname;

            return Json(new { wynik = str });
        }
    }
}
