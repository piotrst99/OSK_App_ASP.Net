using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSK_App.DbContexts;
using OSK_App.Entities;
using OSK_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Controllers
{
    [Route("/Kurs")]
    public class CourseController : Controller
    {
        private readonly ApplicationContext context;

        public CourseController(ApplicationContext context){
            this.context = context;
        }

        [HttpGet]
        [Route("ListaKursow")]
        public IActionResult GetCourses() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                
                var courses = context.courses.ToList<Course>();
                
                foreach(var c in courses) {
                    var courseCategory = context.categories.Where(q => q.ID == c.CategoryID).FirstOrDefault();
                }
                return View("ListOfCourses", courses);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [Route("DodajKurs")]
        public IActionResult AddCourse() {
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
        [Route("DodajKurs")]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(Course c) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                if (!ModelState.IsValid) {
                    var categories = context.categories.ToList<Category>();
                    ViewBag.categories = categories;
                    return View(c);
                }
                else {
                    //c.CourseCost = c.CourseCost;
                    context.courses.Add(c);
                    context.SaveChanges();

                    return RedirectToAction("GetCourses");
                }
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        [Route("EdytujDaneKursu/{id}")]
        public IActionResult Edit(int id) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                var categories = context.categories.ToList<Category>();
                ViewBag.categories = categories;

                var course = context.courses.Where(q => q.ID == id).FirstOrDefault();
                return View("EditCourse", course);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [Route("EdytujDaneKursu/{id}")]
        public IActionResult Edit(Course c) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                var course = context.courses.Where(q => q.ID == c.ID).FirstOrDefault();

                //return Content(c.CourseCost.ToString());

                course.AmountTheoretical = c.AmountTheoretical;
                course.AmountPractical = c.AmountPractical;
                course.CategoryID = c.CategoryID;
                course.CourseCost = c.CourseCost;
                course.Name = c.Name;
                course.ExtraPracticalCost = c.ExtraPracticalCost;

                context.SaveChanges();
                return RedirectToAction("GetCourses");

            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [Route("GetCourseCategory")]
        public IActionResult GetCourseCategory(int categoryId) {
            var courses = context.courses.Where(q => q.CategoryID == categoryId).Select(d=>new Course{ ID=d.ID, Name=d.Name }).ToList<Course>();

            return Json(new { wynik = courses });
        }

        [HttpPost]
        [Route("SetStudentCourse")]
        public IActionResult SetStudentCourse(int categoryID, int courseID, int studentID) {

            var course = context.courses.Where(q => q.ID == courseID).FirstOrDefault();
            course.CountOfStudents += 1;

            StudentCourse studentCourse = new StudentCourse() {
                CourseID = courseID,
                StartDate = DateTime.Now.ToString("dd/MM/yyyy"),
                EndDate = null,
                StudentID = studentID,
                ExtraHours = 0,
                SumOfPayment = 0,
                StudentStatus = "Uczestniczy",
                //StudentCourseStatID = 1,
            }; 

            try {
                context.studentCourses.Add(studentCourse);
                context.SaveChanges();

                return Json(new { result = true });
            }
            catch (Exception e) {

                return Json(new { result = e.ToString() });
            }
        }

        [HttpPost]
        [Route("GetStudentParticipants")]
        public List<MemberCourse> GetStudentParticipants(int id) {
        //public List<StudentCourse> GetStudentParticipants(int id) {
        //public IActionResult GetStudentParticipants(int id) {

            List<MemberCourse> memberCourses = new List<MemberCourse>();

            var participants = context.studentCourses.Where(q => q.CourseID == id).ToList();

            foreach (var member in participants) {

                try {
                    //var student = context.students.Where(q => q.UserID == member.StudentID).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == member.StudentID).FirstOrDefault();

                    MemberCourse memberCourse = new MemberCourse() {
                        Student = studentUser.FirstName + " " + studentUser.Surname,
                        StartDate = member.StartDate,
                        EndDate = member.EndDate,
                        MemberStatus = member.StudentStatus
                    };

                    memberCourses.Add(memberCourse);

                }
                catch (Exception e) {
                    //return Json(new { result = e.ToString() });
                }

            }
            //return Json(new { result = participants });
            //return participants;
            return memberCourses;
        }

    }
}
