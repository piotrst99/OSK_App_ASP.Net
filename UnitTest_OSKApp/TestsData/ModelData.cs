using OSK_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace UnitTest_OSK_App.TestsData
{
    public class ModelData{
        /*public List<Address> addresses { get; set; }
        public List<Category> categories { get; set; }
        public List<Course> courses { get; set; }
        public List<Employee> employees { get; set; }
        public List<Payment> payments { get; set; }
        public List<PaymentStatus> paymentStatuses { get; set; }
        public List<Practical> practicals { get; set; }
        public List<PracticalStatus> practicalStatuses { get; set; }
        public List<Role> roles { get; set; }
        public List<Student> students { get; set; }
        public List<StudentCourse> studentCourses { get; set; }
        public List<Theoretical> theoreticals { get; set; }
        public List<User> users { get; set; }
        public List<Vehicle> vehicles { get; set; }
        public List<VehicleStatus> vehicleStatuses { get; set; }
        public List<TypePayment> typePayments { get; set; }*/

        public List<Address> addresses = new List<Address>();
        public List<Category> categories = new List<Category>();
        public List<Course> courses = new List<Course>();
        public List<Employee> employees = new List<Employee>();
        public List<Payment> payments = new List<Payment>();
        public List<PaymentStatus> paymentStatuses = new List<PaymentStatus>();
        public List<Practical> practicals = new List<Practical>();
        public List<PracticalStatus> practicalStatuses = new List<PracticalStatus>();
        public List<Role> roles = new List<Role>();
        public List<Student> students = new List<Student>();
        public List<StudentCourse> studentCourses = new List<StudentCourse>();
        public List<Theoretical> theoreticals = new List<Theoretical>();
        public List<User> users = new List<User>();
        public List<Vehicle> vehicles = new List<Vehicle>();
        public List<VehicleStatus> vehicleStatuses = new List<VehicleStatus>();
        public List<TypePayment> typePayments = new List<TypePayment>();

        public ModelData() {
            employees.Add(new Employee() { UserID = 1, RoleID = 1, NrInstructor = null, User = new User() { ID = 1, UserName = "WC0001", Password = "12345" } });
            employees.Add(new Employee() { UserID = 2, RoleID = 2, NrInstructor = "XYZ0001", User = new User { ID = 2, UserName = "KG0002", Password = "52179" } });
            employees.Add(new Employee() { UserID = 3, RoleID = 2, NrInstructor = "XYZ0022", User = new User { ID = 3, UserName = "JA0003", Password = "68764" } });
            employees.Add(new Employee() { UserID = 22, RoleID = 2, NrInstructor = "XYZ99999", User = new User { ID = 22, UserName = "PF0022", Password = "90778" } });

            students.Add(new Student() { UserID = 9, User = new User() { ID = 9, UserName = "TK0009", Password = "57913" } });
            students.Add(new Student() { UserID = 8, User = new User() { ID = 8, UserName = "KL0008", Password = "55656" } });
            students.Add(new Student() { UserID = 26, User = new User() { ID = 26, UserName = "AS0026", Password = "38606" } });

            categories.Add(new Category() { ID = 1, Symbol = "B" });
            categories.Add(new Category() { ID = 2, Symbol = "B1" });
            categories.Add(new Category() { ID = 3, Symbol = "C" });
            categories.Add(new Category() { ID = 4, Symbol = "D" });
            categories.Add(new Category() { ID = 5, Symbol = "A" });

            vehicles.Add(new Vehicle() { ID = 1, RegNumber = "KN 99000", Mark = "Suzuki", Model = "Baleno", Course = 0, CategoryID = 1, VehicleStatID = 1 });
            vehicles.Add(new Vehicle() { ID = 2, RegNumber = "KN 00661", Mark = "Suzuki", Model = "Baleno", Course = 0, CategoryID = 1, VehicleStatID = 1 });
            vehicles.Add(new Vehicle() { ID = 3, RegNumber = "KN 33445", Mark = "Triumph", Model = "Street Triple", Course = 0, CategoryID = 5, VehicleStatID = 1 });
            vehicles.Add(new Vehicle() { ID = 4, RegNumber = "KN 45645", Mark = "Suzuki", Model = "Baleno", Course = 0, CategoryID = 1, VehicleStatID = 1 });

            practicals.Add(new Practical() { ID = 1, StudentID = 9, EmployeeID = 2, VehicleID = null, StartTime = "07:00", EndTime = null, Course = 0, PracticalStatID = 1, Category = "B" });
            practicals.Add(new Practical() { ID = 2, StudentID = 8, EmployeeID = 22, VehicleID = null, StartTime = "09:00", EndTime = null, Course = 0, PracticalStatID = 1, Category = "B" });
            practicals.Add(new Practical() { ID = 3, StudentID = 26, EmployeeID = 3, VehicleID = null, StartTime = "11:00", EndTime = null, Course = 0, PracticalStatID = 1, Category = "B" });
        }

        public bool PanelLoginIsExists(string userName, string password) {
            var instructor = employees.Where(q => q.User.UserName == userName && q.User.Password == password && q.RoleID == 1).FirstOrDefault();
            return instructor != null;
        }

        public bool InstructorLoginIsExists(string userName, string password) {
            var instructor = employees.Where(q => q.User.UserName == userName && q.User.Password == password && q.RoleID == 2).FirstOrDefault();
            return instructor != null;
        }

        public bool UserLoginIsExists(string userName, string password) {
            var user = students.Where(q => q.User.UserName == userName && q.User.Password == password).FirstOrDefault();
            return user != null;
        }

        public bool UserDataIsValid(User u) {
            return !String.IsNullOrEmpty(u.FirstName) &&
                !String.IsNullOrEmpty(u.Surname) &&
                u.PESEL.Length == 11;
        }

        public bool DataToAcceptDriveIsValid(Practical p, string nrRegister, string endTime, int? course) {
            var vehicle = vehicles.Where(q => q.RegNumber == nrRegister).FirstOrDefault();
            var category = categories.Where(q => q.Symbol == p.Category).FirstOrDefault();
            return vehicle != null &&
                vehicle.CategoryID == category.ID &&
                course > 0 && course!=null &&
                !String.IsNullOrEmpty(endTime) ? DateTime.ParseExact(endTime, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay > DateTime.ParseExact(p.StartTime, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay : false;

                //DateTime.Parse(endTime, "HH:mm", CultureInfo.InvariantCulture) > DateTime.Parse(p.StartTime, "HH:mm", CultureInfo.InvariantCulture);



        }

    }
}


//users.Add(new User() { ID = 1, UserName = "WC0001", Password = "12345" });
//users.Add(new User() { ID = 2, UserName = "KG0002", Password = "52179", Employee= new Employee() { UserID = 2, RoleID = 2, NrInstructor = "XYZ0001" } });
//users.Add(new User() { ID = 3, UserName = "JA0003", Password = "68764", Employee= new Employee() { UserID = 3, RoleID = 2, NrInstructor = "XYZ0022" } });
//users.Add(new User() { ID = 22, UserName = "PF0022", Password = "90778", Employee= new Employee() { UserID = 22, RoleID = 2, NrInstructor = "XYZ99999" } });
//users.Add(new User() { ID = 8, UserName = "KL0008", Password = "55656" });
//users.Add(new User() { ID = 9, UserName = "TK0009", Password = "57913" });
//users.Add(new User() { ID = 26, UserName = "AS0026", Password = "38606" });