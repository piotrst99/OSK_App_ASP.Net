using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSK_App.DbContexts;
using OSK_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Controllers
{
    [Route("/Pojazd")]
    public class VehicleController : Controller
    {
        private readonly ApplicationContext context;

        public VehicleController(ApplicationContext context) {
            this.context = context;
        }

        [HttpGet]
        [Route("ListaPojazdow")]
        public IActionResult GetVehicles() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                var vehicles = context.vehicles.ToList<Vehicle>();
                foreach(var v in vehicles) {
                    var category = context.categories.Where(q => q.ID == v.CategoryID).FirstOrDefault();
                    var status = context.vehicleStatuses.Where(q => q.ID == v.VehicleStatID).FirstOrDefault();
                }
                return View("ListOfVehicles", vehicles);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [Route("DodajPojazd")]
        public IActionResult AddVehicle() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                var categories = context.categories.ToList<Category>();
                ViewBag.categories = categories;

                return View();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [Route("DodajPojazd")]
        [ValidateAntiForgeryToken]
        public IActionResult AddVehicle(Vehicle v) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                if (!ModelState.IsValid) {
                    return View(v);
                }
                else {
                    v.VehicleStatID = 1;

                    context.vehicles.Add(v);
                    context.SaveChanges();
                
                    return RedirectToAction("GetVehicles");
                }
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        [Route("EdytujDanePojazdu/{id}")]
        public IActionResult Edit(int id) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                
                var vehicle = context.vehicles.Where(q => q.ID == id).FirstOrDefault();

                var categories = context.categories.ToList<Category>();
                ViewBag.categories = categories;

                var statutes = context.vehicleStatuses.ToList<VehicleStatus>();
                ViewBag.statutes = statutes;

                return View("EditVehicle", vehicle);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [Route("EdytujDanePojazdu/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vehicle v) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
            
                if (!ModelState.IsValid) {
                    return View("EditVehicle", v);
                }
                else {

                    var vehicle = context.vehicles.Where(q => q.ID == v.ID).FirstOrDefault();

                    vehicle.RegNumber = v.RegNumber;
                    vehicle.Mark = v.Mark;
                    vehicle.Model = v.Model;
                    vehicle.Course = v.Course;
                    vehicle.CategoryID = v.Category.ID;
                    vehicle.VehicleStatID = v.VehicleStatus.ID;

                    context.SaveChanges();

                    return RedirectToAction("GetVehicles");
                }
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpDelete]
        //[HttpGet]
        [Route("UsunPojazd/{id}")]
        public IActionResult RemoveVehicle(int id) {

            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                var vehicle = context.vehicles.Where(q => q.ID == id).FirstOrDefault();

                //return Content(vehicle.RegNumber + " " + vehicle.Mark + " " + vehicle.Model);
                context.vehicles.Remove(vehicle);
                context.SaveChanges();
                return RedirectToAction("GetVehicles");
            }
            else {
                return RedirectToAction("Login", "Home");
            }

        }

    }
}
