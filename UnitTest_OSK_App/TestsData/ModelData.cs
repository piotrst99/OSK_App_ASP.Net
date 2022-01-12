using OSK_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            users.Add(new User() { ID = 1, UserName = "WC0001", Password = "12345" });
            users.Add(new User() { ID = 2, UserName = "KG0002", Password = "52179" });
            users.Add(new User() { ID = 3, UserName = "JA0003", Password = "68764" });
            users.Add(new User() { ID = 9, UserName = "TK0009", Password = "57913" });
            users.Add(new User() { ID = 26, UserName = "AS0026", Password = "38606" });

            employees.Add(new Employee() { UserID = 1, RoleID = 1, NrInstructor = null });
            employees.Add(new Employee() { UserID = 2, RoleID = 2, NrInstructor = "XYZ0001" });
            employees.Add(new Employee() { UserID = 3, RoleID = 2, NrInstructor = "XYZ0022" });

            students.Add(new Student() { UserID = 9 });
            students.Add(new Student() { UserID = 26 });
        }

    }
}
