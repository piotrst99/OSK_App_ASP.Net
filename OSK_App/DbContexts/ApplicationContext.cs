using Microsoft.EntityFrameworkCore;
using OSK_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSK_App.Models;

namespace OSK_App.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) {  }

        public DbSet<Address> addresses { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<PaymentStatus> paymentStatuses { get; set; }
        public DbSet<Practical> practicals { get; set; }
        public DbSet<PracticalStatus> practicalStatuses { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<StudentCourse> studentCourses { get; set; }
        //public DbSet<StudentCourseStatus> studentCourseStatuses { get; set; }
        public DbSet<Theoretical> theoreticals { get; set; }
        public DbSet <User> users { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<VehicleStatus> vehicleStatuses { get; set; }
        public DbSet<TypePayment> typePayments { get; set; }
        //public DbSet<OSK_App.Models.StudentDetails> StudentPayment { get; set; }
        //public DbSet<OSK_App.Models.EmployeeDetails> EmployeeDetails { get; set; }
    }
}
