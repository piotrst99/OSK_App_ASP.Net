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
                return View("ListOfPayments", payments);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        [Route("PrzyjmijWplate")]
        public IActionResult AcceptPayment(){
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                return View();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [Route("AcceptPayment")]
        public IActionResult AcceptPayment(int studentID, int cost, int typePaimentID) {
            int employeeID = (Int32)HttpContext.Session.GetInt32("_Id");
            string s = cost.ToString() + " " + DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("HH:mm") +
                " " + studentID.ToString() + " " + employeeID.ToString() + " " + typePaimentID.ToString();

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

            try {
                context.payments.Add(p);
                context.SaveChanges();
                return Json(new { isSave = true });
            }
            catch (Exception e) {
                return Json(new { isSave = e.ToString() });
            }
        }

        [HttpPost]
        [Route("Test")]
        public IActionResult Test(int userID, double cost, int typePaimentID) {
            int e = (Int32)HttpContext.Session.GetInt32("_Id");
            string s = cost.ToString() + " " + DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("HH:mm") +
                " " + userID.ToString() + " " + e.ToString() + " " + typePaimentID.ToString();
            return Json(new { wynik = s });
            //return Content(s);
        }

    }
}
