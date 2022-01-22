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
    [Route("/Wplata")]
    public class PaymentController : Controller
    {
        private readonly ApplicationContext context;

        public PaymentController(ApplicationContext context) {
            this.context = context;
        }

        [HttpGet]
        [Route("Wplaty")]
        public IActionResult GetPayments() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                
                var payments = context.payments.ToList<Payment>();

                foreach (var p in payments) {
                    var student = context.students.Where(q => q.UserID == p.StudentID).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == student.UserID).FirstOrDefault();
                    var employee = context.employees.Where(q => q.UserID == p.EmployeeID).FirstOrDefault();
                    var employeeUser = context.users.Where(q => q.ID == employee.UserID).FirstOrDefault();
                    var typePaiment = context.typePayments.Where(q => q.ID == p.TypePaymentID).FirstOrDefault();
                }

                return View("ListOfPayments", payments);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        /*[HttpGet]
        [Route("PrzyjmijWplate")]
        public IActionResult AcceptPayment(){
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                return View();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }*/

        [HttpPost]
        [Route("AcceptPayment")]
        public IActionResult AcceptPayment(int studentID, int cost, int typePaimentID) {
            int employeeID = (Int32)HttpContext.Session.GetInt32("_Id");

            var studentCourse = context.studentCourses.Where(q => q.StudentID == studentID).FirstOrDefault();
            if(typePaimentID == 1) studentCourse.SumOfPayment += cost;

            Payment p = new Payment() {
                Cost = cost,
                PaymentDate = DateTime.Now.ToString("dd/MM/yyyy"),
                PaymentTime = DateTime.Now.ToString("HH:mm"),
                ReceiptDate = DateTime.Now.ToString("dd/MM/yyyy"),
                StudentID = studentID,
                EmployeeID = employeeID,
                PaymentStatID = 1,
                TypePaymentID = typePaimentID
            };

            /*if (typePaimentID == 2) {
                var coure = context.courses.Where(q => q.ID == studentCourse.CourseID).FirstOrDefault();
                studentCourse.ExtraHours += cost / coure.ExtraPracticalCost;
            }*/

            try {
                context.payments.Add(p);
                context.SaveChanges();
                return Json(new { isSave = true });
            }
            catch (Exception e) {
                return Json(new { isSave = e.ToString() });
            }
        }

        [HttpGet]
        [Route("ListaKursantowWplacone")]
        public IActionResult GetStudentsPaymentEntireCourse() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                List<StudentCourse> studentsCourseEntireCourse = new List<StudentCourse>();
                var studentCourse = context.studentCourses.ToList();

                foreach (var s in studentCourse) {

                    var course = context.courses.Where(q => q.ID == s.CourseID).FirstOrDefault();
                    var student = context.students.Where(q => q.UserID == s.StudentID).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == student.UserID).FirstOrDefault();

                    if (s.SumOfPayment == (int)course.CourseCost) {
                        studentsCourseEntireCourse.Add(s);
                    }

                }

                return View("ListOfStudentPay", studentsCourseEntireCourse);
            
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        [Route("ListaKursantowNieWplacone")]
        public IActionResult GetStudentNotPaymentEntireCourse() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                List<StudentCourse> studentsCourseNotEntireCourse = new List<StudentCourse>();
                var studentCourse = context.studentCourses.ToList();
                

                foreach (var s in studentCourse) {

                    var course = context.courses.Where(q => q.ID == s.CourseID).FirstOrDefault(); 
                    var student = context.students.Where(q => q.UserID == s.StudentID).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == student.UserID).FirstOrDefault();

                    if (s.SumOfPayment < (int)course.CourseCost) {
                        studentsCourseNotEntireCourse.Add(s);
                    }

                }

                return View("ListOfStudentPayNot", studentsCourseNotEntireCourse);

            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpDelete]
        [Route("UsunPlatnosc/{id}")]
        public IActionResult RemovePayment(int id) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                var payment = context.payments.Where(q => q.ID == id).FirstOrDefault();

                //return Content(user.FirstName + " " + user.Surname);
                context.payments.Remove(payment);
                return RedirectToAction("GetPayments");
            }
            else {
                return RedirectToAction("Login", "Home");
            }

        }

    }
}
