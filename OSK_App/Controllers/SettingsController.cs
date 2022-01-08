using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSK_App.DbContexts;
using OSK_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Controllers
{
    [Route("Ustawienia")]
    public class SettingsController : Controller
    {
        private readonly ApplicationContext context;

        public SettingsController(ApplicationContext context) {
            this.context = context;
        }

        [HttpGet]
        [Route("Konto")]
        public IActionResult Account() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                int id = (Int32)HttpContext.Session.GetInt32("_Id");

                var employee = context.employees.Where(q => q.UserID == id).FirstOrDefault();
                var employeeRole = context.roles.Where(q => q.ID == employee.RoleID).FirstOrDefault();
                var user = context.users.Where(q => q.ID == employee.UserID).FirstOrDefault();
                var userAddress = context.addresses.Where(q => q.ID == user.AddressID).FirstOrDefault();
                var employeePracticals = context.practicals.Where(q => q.EmployeeID == id).ToList();

                foreach (var ePractical in employeePracticals) {
                    var student = context.students.Where(q => q.UserID == ePractical.StudentID).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == student.UserID).FirstOrDefault();
                    var practicalStatus = context.practicalStatuses.Where(q => q.ID == ePractical.PracticalStatID).FirstOrDefault();
                }

                var employeeDetailsModel = new EmployeeDetails() {
                    ID = employee.UserID,
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Surname = user.Surname,
                    PESEL = user.PESEL,
                    UserName = user.UserName,

                    Street = userAddress.Street,
                    NrHome = user.Address.NrHome,
                    NrLocal = user.Address.NrLocal,
                    Town = user.Address.Town,
                    PostCode = user.Address.PostCode,
                    Post = user.Address.Post,
                    PhoneNumber = user.Address.PhoneNumber,
                    Email = user.Address.Email,

                    RoleName = employeeRole.Name,
                    NrInstructor = employee.NrInstructor,

                    practicals = employeePracticals
                };

                return View("Account", employeeDetailsModel);
                
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
