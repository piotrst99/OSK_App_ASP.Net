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
    //[ApiController]
    [Route("/Pracownik")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationContext context;

        public EmployeeController(ApplicationContext context) {
            this.context = context;
        }

        [HttpGet]
        [Route("ListaPracownikow")]
        public IActionResult GetEmployees() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
            
                var employees = context.employees.ToList<Employee>();

                foreach (var i in employees) {
                    var user = context.users.Where(q => q.ID == i.UserID).FirstOrDefault();
                }

                foreach (var r in employees) {
                    var role = context.roles.Where(q => q.ID == r.RoleID).FirstOrDefault();
                }

                return View("ListOfEmployees", employees);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [Route("DodajPracownika")]
        public IActionResult AddEmployee() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
            
                var roles = context.roles.ToList<Role>();
                ViewBag.roles = roles;

                return View();            
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [Route("DodajPracownika")]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee e) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
            
                var getUserID = context.users.Count();
                getUserID += 1;

                e.User.UserName = LoginPasswordGenerate.CreateLogin(e.User.FirstName, e.User.Surname, getUserID);
                e.User.Password = LoginPasswordGenerate.CreatePassword();

                context.employees.Add(e);
                context.SaveChanges();

                return RedirectToAction("GetEmployees");
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        [Route("EdytujDanePracownika/{id}")]
        public IActionResult Edit(int id) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
            
                var roles = context.roles.ToList<Role>();
                ViewBag.roles = roles;
                try {
                    var employee = context.employees.Where(q => q.UserID == id).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == employee.UserID).FirstOrDefault();
                    var userAddress = context.addresses.Where(q => q.ID == studentUser.AddressID).FirstOrDefault();
                    
                    return View("EditEmployee", employee);
                }
                catch (Exception) {
                    ViewBag.ErrorMessage = "Błąd! Niepoprawne dane.";
                    return View("ErrorMessage");
                }

            }
            else {
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        [Route("EdytujDanePracownika/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee e) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                if (!ModelState.IsValid) {
                    return View("EditEmployee", e);
                }
                else {

                    var employee = context.employees.Where(q => q.UserID == e.UserID).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == employee.UserID).FirstOrDefault();
                    var userAddress = context.addresses.Where(q => q.ID == studentUser.AddressID).FirstOrDefault();

                    employee.RoleID = e.RoleID;
                    employee.NrInstructor = e.NrInstructor;

                    employee.User.FirstName = e.User.FirstName;
                    employee.User.SecondName = e.User.SecondName;
                    employee.User.Surname = e.User.Surname;
                    employee.User.PESEL = e.User.PESEL;
            
                    employee.User.Address.Street = e.User.Address.Street;
                    employee.User.Address.NrHome = e.User.Address.NrHome;
                    employee.User.Address.NrLocal = e.User.Address.NrLocal;
                    employee.User.Address.Town = e.User.Address.Town;
                    employee.User.Address.PostCode = e.User.Address.PostCode;
                    employee.User.Address.Post = e.User.Address.Post;
                    employee.User.Address.PhoneNumber = e.User.Address.PhoneNumber;
                    employee.User.Address.Email = e.User.Address.Email;

                    context.SaveChanges();

                    return RedirectToAction("GetEmployees");
                }
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        //[HttpDelete]
        [HttpGet]
        [Route("UsunPracownika/{id}")]
        public IActionResult RemoveEmployee(int id) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                var employee = context.employees.Where(q => q.UserID == id).FirstOrDefault();
                var user = context.users.Where(q => q.ID == employee.UserID).FirstOrDefault();

                return Content(user.FirstName + " " + user.Surname);
                //return RedirectToAction("GetStudents");
            }
            else {
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpGet]
        [Route("SzczegolyPracownika/{id}")]
        public IActionResult GetDetailsEmployee(int id){
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

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

                return View("DetailsEmployee", employeeDetailsModel);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

    }
}
