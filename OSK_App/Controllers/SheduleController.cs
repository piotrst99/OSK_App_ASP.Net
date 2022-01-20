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
    [Route("Zajecia")]
    public class SheduleController : Controller
    {
        private readonly ApplicationContext context;

        public SheduleController(ApplicationContext context) {
            this.context = context;
        }
        
        [Route("Zajecia")]
        public IActionResult Shedules() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                return View();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [Route("ZajeciaTeoretyczne")]
        public IActionResult TheoreticalShedules() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                var theoretical = context.theoreticals.ToList();

                foreach (var t in theoretical) {
                    var employee = context.employees.Where(q => q.UserID == t.EmployeeID).FirstOrDefault();
                    var employeeUser = context.users.Where(q => q.ID == employee.UserID).FirstOrDefault();
                }

                return View(theoretical);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        ///////////////////////////

        [Route("GetZajeciaPraktyczne4")]
        public IActionResult GetZajeciaPraktyczne4(string wybranaData) {
            string strRes = "";
            var zajeciaPraktyczne = context.practicals.Where(q => q.Data == wybranaData).ToList();

            List<Student> kursant2 = new List<Student>();
            List<User> osoba = new List<User>();

            foreach (var z in zajeciaPraktyczne) {
                var kursant = context.students.Where(q => q.UserID == z.StudentID).FirstOrDefault();
                kursant2.Add(kursant);
            }

            foreach (var k in kursant2) {
                var userKur = context.users.
                    Select(d => new User{
                        ID = d.ID,
                        UserName = null,
                        Password = null,
                        FirstName= d.FirstName,
                        SecondName = null,
                        Surname = d.Surname,
                        PESEL = null,
                        Address = null,
                        Student = null,
                        Employee = null,
                    }).Where(q => q.ID == k.UserID).FirstOrDefault();
                osoba.Add(userKur);
            }

            for (int i = 0; i < zajeciaPraktyczne.Count; i++) {
                zajeciaPraktyczne[i].Student.User = osoba[i];
            }

            if (zajeciaPraktyczne.Count() == 0) {
                strRes += "Nikt nie ma zajęć w tym dniu";
            }
            else {
                foreach (var i in zajeciaPraktyczne) {
                    strRes += i.Data + " " + i.StartTime + "<br/>";
                }
            }

            return Json(new { wynik = zajeciaPraktyczne });
        }

        [Route("GetOsobyZajeciaPraktyczne")]
        public IActionResult GetOsobyZajeciaPraktyczne() {
            List<Student> kursanci = context.students.ToList<Student>();
            List<Employee> instruktorzy = context.employees.Where(q=>q.RoleID == 2).ToList<Employee>();
            List<User> osoba = new List<User>();


            foreach (var kursant in kursanci) {
                var user = context.users.Select(d => new User{
                    ID = d.ID,
                    FirstName= d.FirstName,
                    Surname = d.Surname,
                }).Where(q => q.ID == kursant.UserID).FirstOrDefault();
                osoba.Add(user);
            }

            for (int i = 0; i < kursanci.Count; i++) {
                kursanci[i].User = osoba[i];
            }

            osoba.Clear();


            foreach (var instruktor in instruktorzy) {
                var user = context.users.Where(q => q.ID == instruktor.UserID).FirstOrDefault();
            }

            foreach (var i in instruktorzy) {
                var user = context.users.Select(d => new User{
                    ID = d.ID,
                    FirstName = d.FirstName,
                    Surname = d.Surname,
                }).Where(q => q.ID == i.UserID).FirstOrDefault();
                osoba.Add(user);
            }

            for (int i = 0; i < instruktorzy.Count; i++) {
                instruktorzy[i].User = osoba[i];
            }

            return Json(new { listaKursantow = kursanci, listaInstruktorow = instruktorzy });
        }

        [Route("GetZajeciaPraktyczne5")]
        public IActionResult GetZajeciaPraktyczne5(int idZajec) {
            var zajeciaPraktyczne = context.practicals.Where(q => q.ID == idZajec).FirstOrDefault();
            var kursant = context.students.Where(q => q.UserID == zajeciaPraktyczne.StudentID).FirstOrDefault();
            var userKursant = context.users.Select(q => new User { ID = q.ID, FirstName= q.FirstName, Surname= q.Surname}).Where(d => d.ID == kursant.UserID).FirstOrDefault();
            var instruktor = context.employees.Where(q => q.UserID == zajeciaPraktyczne.EmployeeID).FirstOrDefault();
            var userInstruktor = context.users.Select(q => new User { ID = q.ID, FirstName = q.FirstName, Surname = q.Surname }).Where(d => d.ID == instruktor.UserID).FirstOrDefault();
            var pojazd = context.vehicles.Where(q => q.ID == zajeciaPraktyczne.VehicleID).FirstOrDefault();

            kursant.User = userKursant;
            instruktor.User = userInstruktor;

            return Json(new { wynik = zajeciaPraktyczne });
        }

        [Route("setZeciaPraktyczne")]
        public IActionResult setZeciaPraktyczne(string data, int kursantID, int instruktorID, string godzStart) {

            var studentCourseID = context.studentCourses.Where(q => q.StudentID == kursantID).Select(q => q.CourseID).FirstOrDefault();
            var course = context.courses.Where(q => q.ID == studentCourseID).FirstOrDefault();
            var category = context.categories.Where(q => q.ID == course.CategoryID).FirstOrDefault();

            var zajeciaPraktyczne = new Practical() {
                StudentID = kursantID,
                EmployeeID = instruktorID,
                VehicleID = null,
                Data = data,
                StartTime = godzStart,
                EndTime = null,
                Course = 0,
                PracticalStatID = 1,
                IsCancel = false,
                Category = category.Symbol
            };

            context.practicals.Add(zajeciaPraktyczne);

            try {
                context.SaveChanges();
            }
            catch (Exception e) {
                return Json(new { czyDodano = false, t = e.ToString() });
                //return Content(e.ToString());
            }
            
            return Json(new { czyDodano = true });

        }

        [Route("deleteZajeciaPraktyczne")]
        public IActionResult deleteZajeciaPraktyczne(int idZajPrak) {

            var zajPrak = context.practicals.Where(q => q.ID == idZajPrak).FirstOrDefault();
            context.practicals.Remove(zajPrak);
            context.SaveChanges();
            
            return Json(new { czyUsunieto = true });

        }


        [Route("GetPracticalData")]
        public IActionResult GetPracticalData(int id) {

            try {
                var studentID = context.practicals.Where(q => q.ID == id).Select(q => q.StudentID).FirstOrDefault();
                var studentCourseID = context.studentCourses.Where(q => q.StudentID == studentID).Select(q => q.CourseID).FirstOrDefault();
                var course = context.courses.Where(q => q.ID == studentCourseID).FirstOrDefault();
                var category = context.categories.Where(q => q.ID == course.CategoryID).FirstOrDefault();
                return Json(new { result = category.Symbol });

            }
            catch (Exception e) {
                return Json(new { result = e.ToString() });
            }

        }

        [Route("EditPracticalData")]
        public IActionResult EditPracticalData(int practicalID, string endTime, int course, int practicalStatusID) {

            try {
                var practical = context.practicals.Where(q => q.ID == practicalID).FirstOrDefault();

                practical.EndTime = endTime;
                practical.Course = course;
                practical.PracticalStatID = practicalStatusID;

                context.SaveChanges();
                return Json(new { isEdit = true });
            }
            catch (Exception e) {
                return Json(new { isEdit = e.ToString() });
            }

        }

    }
}
