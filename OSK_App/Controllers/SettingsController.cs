using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Controllers
{
    [Route("Ustawienia")]
    public class SettingsController : Controller
    {
        public IActionResult Index() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                
                //return View();
                return Content("Ustawnienia");
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
