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
    [Route("/Kursant")]
    public class StudentController : Controller
    {
        private readonly ApplicationContext context;

        public StudentController(ApplicationContext context) {
            this.context = context;
        }

        [HttpGet("/{surname}")]
        [Route("ListaKursantow")]
        public IActionResult GetStudents(string surname) {
        //public IActionResult GetStudents() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                
                var students = context.students.ToList<Student>();

                if (surname == null) {
                    foreach (var s in students) {
                        var user = context.users.Where(q => q.ID == s.UserID).FirstOrDefault();
                    }
                    ViewBag.Surname = null;
                }
                else {
                    var users = context.users.Where(q => q.Surname.Contains(surname)).ToList();

                    foreach (var s in students) {

                        for (int i = 0; i < users.Count; i++) {
                            if (users[i].ID == s.UserID) {
                                s.User = new User() {
                                    ID = users[i].ID,
                                    FirstName = users[i].FirstName,
                                    SecondName = users[i].SecondName,
                                    Surname = users[i].Surname,
                                    PESEL = users[i].PESEL,
                                    UserName = users[i].UserName
                                };
                                i = users.Count;
                            }
                            else {
                                s.User = new User() {
                                    FirstName = "test",
                                    SecondName = "test",
                                    Surname = "test",
                                    PESEL = "test",
                                    UserName = "test"
                                };
                            }
                        }
                        ViewBag.Surname = surname;
                    }

                    students = students.Where(q => q.User.FirstName != "test").ToList();

                }

                return View("ListOfStudents", students);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet("/{surname}")]
        //[HttpGet]
        [Route("GetStudentsFromSearch")]
        public IActionResult GetStudentsFromSearch(string surname) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                var students = context.students.ToList<Student>();

                //return Content("Nazwisko: "+surname);

                var users = context.users.Where(q => q.Surname == surname).ToList();

                try {
                    foreach (var s in students) {
                        //var user = context.users.Where(q => q.ID == s.UserID && q.Surname == surname).FirstOrDefault();

                        for (int i = 0; i < users.Count; i++) {
                            if(users[i].ID == s.UserID){
                                s.User = new User() {
                                    FirstName = users[i].FirstName,
                                    SecondName = users[i].SecondName,
                                    Surname = users[i].Surname,
                                    PESEL = users[i].PESEL,
                                    UserName = users[i].UserName
                                };
                            }
                            else {
                                s.User = new User() {
                                    FirstName = "test",
                                    SecondName = "test",
                                    Surname = "test",
                                    PESEL = "test",
                                    UserName = "test"
                                };
                            }
                        }

                        /*foreach (var u in users) {
                            if(s.UserID == u.ID){
                                s.User = new User() {
                                    FirstName = u.FirstName,
                                    SecondName = u.SecondName,
                                    Surname = u.Surname,
                                    PESEL = u.PESEL,
                                    UserName = u.UserName,
                                };
                            }
                        }*/
                        //return Content(s.User.ToString());
                    }
                }
                catch (Exception) {

                    throw;
                }
                students = students.Where(q => q.User.FirstName != "test").ToList();
                //return Content(students.ToString());
                //return RedirectToAction("GetStudents", "Home");
                return View("ListOfStudents", students);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [Route("DodajKursanta")]
        public IActionResult AddStudent() {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                return View();
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [Route("DodajKursanta")]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(Student s) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                if (!ModelState.IsValid) {
                    return View(s);
                }
                else {
                    var getUserID = context.users.Count();
                    getUserID += 1;

                    s.User.UserName = LoginPasswordGenerate.CreateLogin(s.User.FirstName, s.User.Surname, getUserID);
                    s.User.Password = LoginPasswordGenerate.CreatePassword();

                    context.students.Add(s);
                    context.SaveChanges();

                    return RedirectToAction("GetStudents");
                }
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        [Route("SzczegolyKursanta/{id}")]
        public IActionResult GetDetailsStudent(int id) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                var user = context.users.Where(q => q.ID == id).FirstOrDefault();
                var userAddress = context.addresses.Where(q => q.ID == user.AddressID).FirstOrDefault();
                var userPayments = context.payments.Where(q => q.StudentID == user.ID).ToList();
                var studentCourses = context.studentCourses.Where(q => q.StudentID == user.ID).ToList();
                var studentPracticals = context.practicals.Where(q => q.StudentID == user.ID).ToList();

                foreach (var userPay in userPayments) {
                    var employee = context.employees.Where(q => q.UserID == userPay.EmployeeID).FirstOrDefault();
                    var employeeUser = context.users.Where(q => q.ID == employee.UserID).FirstOrDefault();
                    var typePayment = context.typePayments.Where(q => q.ID == userPay.TypePaymentID).FirstOrDefault();
                }

                foreach (var sCourse in studentCourses) {
                    var course = context.courses.Where(q => q.ID == sCourse.CourseID).FirstOrDefault();
                    var cateory = context.categories.Where(q => q.ID == course.CategoryID).FirstOrDefault();
                    //var courseStatus = context.studentCourseStatuses.Where(q => q.ID == sCourse.StudentCourseStatID).FirstOrDefault();
                }

                foreach(var sPractical in studentPracticals) {
                    var employee = context.employees.Where(q => q.UserID == sPractical.EmployeeID).FirstOrDefault();
                    var emplourrUser = context.users.Where(q => q.ID == employee.UserID).FirstOrDefault();
                    var status = context.practicalStatuses.Where(q => q.ID == sPractical.PracticalStatID).FirstOrDefault();
                }

                var userDetailsModel = new StudentDetails() {
                    ID = user.ID,
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

                    payments = userPayments,
                    studentCourses = studentCourses,
                    practicals = studentPracticals
                };


                var typePayments = context.typePayments.ToList<TypePayment>();
                ViewBag.typePayments = typePayments;

                var categories = context.categories.ToList<Category>();
                ViewBag.categories = categories;

                return View("DetailsStudent", userDetailsModel);
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

            Payment p = new Payment() {
                //Cost = Convert.ToDecimal(cost),
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


            //return Content("test");
        }

        [HttpGet]
        [Route("EdytujDaneKursanta/{id}")]
        public IActionResult Edit(int id) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                try {
                    var student = context.students.Where(q => q.UserID == id).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == student.UserID).FirstOrDefault();
                    var userAddress = context.addresses.Where(q => q.ID == studentUser.AddressID).FirstOrDefault();
                
                    return View("EditStudent", student);
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
        [Route("EdytujDaneKursanta/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student s) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");

                if (!ModelState.IsValid) {
                    return View("EditStudent", s);
                }
                else {
                    var student = context.students.Where(q => q.UserID == s.UserID).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == student.UserID).FirstOrDefault();
                    var userAddress = context.addresses.Where(q => q.ID == studentUser.AddressID).FirstOrDefault();

                    student.User.FirstName = s.User.FirstName;
                    student.User.SecondName = s.User.SecondName;
                    student.User.Surname = s.User.Surname;
                    student.User.PESEL = s.User.PESEL;

                    student.User.Address.Street = s.User.Address.Street;
                    student.User.Address.NrHome = s.User.Address.NrHome;
                    student.User.Address.NrLocal = s.User.Address.NrLocal;
                    student.User.Address.Town = s.User.Address.Town;
                    student.User.Address.PostCode = s.User.Address.PostCode;
                    student.User.Address.Post = s.User.Address.Post;
                    student.User.Address.PhoneNumber = s.User.Address.PhoneNumber;
                    student.User.Address.Email = s.User.Address.Email;

                    context.SaveChanges();

                    return RedirectToAction("GetStudents");
                }
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        //[HttpDelete]
        [HttpGet]
        [Route("UsunKursanta/{id}")]
        public IActionResult RemoveStudent(int id) {
            if (HttpContext.Session.GetInt32("_Id") != null) {
                TempData["UserId"] = (Int32)HttpContext.Session.GetInt32("_Id");
                
                var student = context.students.Where(q => q.UserID == id).FirstOrDefault();
                var user = context.users.Where(q => q.ID == student.UserID).FirstOrDefault();

                return Content(user.FirstName + " " + user.Surname);
                //return RedirectToAction("GetStudents");
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

    }
}
