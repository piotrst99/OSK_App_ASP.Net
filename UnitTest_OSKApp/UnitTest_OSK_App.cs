//using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSK_App;
using OSK_App.Entities;
using System.Collections.Generic;
using System.Linq;
using UnitTest_OSK_App.TestsData;

namespace UnitTest_OSK_App
{
    [TestClass]
    public class UnitTest_OSK_App
    {
        public ModelData modelData = new ModelData();

        [TestMethod]
        public void CreateLoginFromUserName_ShouldBeOk() {
            Assert.AreEqual(UserLogin.CreateLogin("Piotr", "Nowak", 1), "PN0001");
            Assert.AreEqual(UserLogin.CreateLogin("£ukasz", "Tort", 22), "LT0022");
            Assert.AreEqual(UserLogin.CreateLogin("Filip", "£¹cki", 143), "FL0143");
            Assert.AreEqual(UserLogin.CreateLogin("¯aneta", "¯arnowska", 3245), "ZZ3245");
            Assert.AreEqual(UserLogin.CreateLogin("Karol", "Dynia", 45754), "KD45754");
            Assert.AreEqual(UserLogin.CreateLogin("Ryszard", "¯¹d³o", 123456), "RZ123456");
        }

        [TestMethod]
        public void LoginToPanel_ShouldBeOK() {
            Assert.IsTrue(modelData.PanelLoginIsExists("WC0001", "12345"));
        }

        [TestMethod]
        public void LoginToPanel_ShouldNotBeOK() {
            Assert.IsFalse(modelData.PanelLoginIsExists("KG0002", "52179"));
            Assert.IsFalse(modelData.PanelLoginIsExists("TK0009", "57913"));
        }

        [TestMethod]
        public void LoginToStudentAccount_ShouldBeOK() {
            Assert.IsTrue(modelData.UserLoginIsExists("TK0009", "57913"));
            Assert.IsTrue(modelData.UserLoginIsExists("AS0026", "38606"));
        }

        [TestMethod]
        public void LoginToStudentAccount_ShouldNotBeOK() {
            Assert.IsFalse(modelData.UserLoginIsExists("WC0001", "12345"));
            Assert.IsFalse(modelData.UserLoginIsExists("JA0003", "68764"));
            Assert.IsFalse(modelData.UserLoginIsExists("", ""));
            Assert.IsFalse(modelData.UserLoginIsExists("", "12345"));
            Assert.IsFalse(modelData.UserLoginIsExists("KL0008", ""));
        }

        [TestMethod]
        public void LoginToInstructorAccount_ShouldBeOK() {
            Assert.IsTrue(modelData.InstructorLoginIsExists("KG0002", "52179"));
            Assert.IsTrue(modelData.InstructorLoginIsExists("PF0022", "90778"));
        }

        [TestMethod]
        public void LoginToInstructorAccount_ShouldNotBeOK() {
            Assert.IsFalse(modelData.InstructorLoginIsExists("KL0008", "55656"));
            Assert.IsFalse(modelData.InstructorLoginIsExists("JA0003", ""));
            Assert.IsFalse(modelData.InstructorLoginIsExists("WC0001", "12345"));
            Assert.IsFalse(modelData.InstructorLoginIsExists("", ""));
        }

        [TestMethod]
        public void AddUserData_SouldBeValid() {
            User u = new User() { FirstName = "Piotr", Surname = "Góra", PESEL = "99999999999" };
            Assert.IsTrue(modelData.UserDataIsValid(u));
        }

        [TestMethod]
        public void AddUserData_SouldNotBeValid() {
            User u = new User() { FirstName = "Piotr", Surname = "Góra", PESEL = "" };
            User u2 = new User() { FirstName = "Agata", Surname = "", PESEL = "00888888888" };
            User u3 = new User() { FirstName = "", Surname = "Dom", PESEL = "84333333333" };
            User u4 = new User() { FirstName = "Wojciech", Surname = "", PESEL = "" };
            User u5 = new User() { FirstName = "", Surname = "Metal", PESEL = "" };
            User u6 = new User() { FirstName = "", Surname = "", PESEL = "02444444444" };
            User u7 = new User() { FirstName = "", Surname = "", PESEL = "" };
            User u8 = new User() { FirstName = "Eryk", Surname = "Sok", PESEL = "00" };
            Assert.IsFalse(modelData.UserDataIsValid(u));
            Assert.IsFalse(modelData.UserDataIsValid(u2));
            Assert.IsFalse(modelData.UserDataIsValid(u3));
            Assert.IsFalse(modelData.UserDataIsValid(u4));
            Assert.IsFalse(modelData.UserDataIsValid(u5));
            Assert.IsFalse(modelData.UserDataIsValid(u6));
            Assert.IsFalse(modelData.UserDataIsValid(u7));
            Assert.IsFalse(modelData.UserDataIsValid(u8));
        }

        [TestMethod]
        public void SaveDriveData_ShouldBeVaid() {
            var p1 = modelData.practicals.Where(q => q.ID == 1).FirstOrDefault();
            var p2 = modelData.practicals.Where(q => q.ID == 2).FirstOrDefault();
            Assert.IsTrue(modelData.DataToAcceptDriveIsValid(p1, "KN 99000", "09:00", 21));
            Assert.IsTrue(modelData.DataToAcceptDriveIsValid(p1, "KN 45645", "11:00", 19));
        }

        [TestMethod]
        public void SaveDriveData_ShouldNotBeVaid() {
            var p1 = modelData.practicals.Where(q => q.ID == 1).FirstOrDefault();
            var p2 = modelData.practicals.Where(q => q.ID == 2).FirstOrDefault();
            Assert.IsFalse(modelData.DataToAcceptDriveIsValid(p1, "KN 99000", "06:55", -3));
            Assert.IsFalse(modelData.DataToAcceptDriveIsValid(p2, "KN 00000", "11:00", 22));
            Assert.IsFalse(modelData.DataToAcceptDriveIsValid(p1, "", "11:00", 22));
            Assert.IsFalse(modelData.DataToAcceptDriveIsValid(p1, "KN 99000", "", 2));
            Assert.IsFalse(modelData.DataToAcceptDriveIsValid(p2, "KN 99000", "11:00", null));
            Assert.IsFalse(modelData.DataToAcceptDriveIsValid(p2, "", "", null));
            Assert.IsFalse(modelData.DataToAcceptDriveIsValid(p1, "", "05:00", -20));
            Assert.IsFalse(modelData.DataToAcceptDriveIsValid(p1, "KN 33445", "9:02", 30));
        }


        /*[TestMethod]
        public void LoginToLoginPanel_ShouldBeOK2() {
            var optionsBuilder = new DbContextOptionsBuilder<OSK_App.DbContexts.ApplicationContext>().UseSqlServer("Data Source = localhost; Initial Catalog = OSKDatabase; Integrated Security = True");

            using (var db = new OSK_App.DbContexts.ApplicationContext(optionsBuilder.Options)) {
                var employee = db.employees.Where(q => q.User.UserName == "WC0001" && q.User.Password == "w" && q.Role.ID == 1).FirstOrDefault();
                Assert.IsTrue(employee!=null);
                var employee2 = db.employees.Where(q => q.User.UserName == "MZ0021" && q.User.Password == "14618" && q.Role.ID == 1).FirstOrDefault();
                Assert.IsTrue(employee!=null);
            }

        }*/

        /*public bool test(string u, string p) {
            var optionsBuilder = new DbContextOptionsBuilder<OSK_App.DbContexts.ApplicationContext>().UseSqlServer("OSK_Context");

            using (var db = new OSK_App.DbContexts.ApplicationContext(optionsBuilder.Options)) {
                var employee = db.employees.Where(q => q.User.UserName == u && q.User.Password == p && q.Role.ID == 1).FirstOrDefault();

                if (employee != null) {
                    return true;
                }
                else {
                    return false;
                }

            }

        }*/

    }
}


// https://docs.microsoft.com/pl-pl/aspnet/core/test/integration-tests?view=aspnetcore-6.0






/*User u = new User { ID = 1, UserName = "WC0001", Password = "12345" };
User u2 = new User { ID = 3, UserName = "JA0003", Password = "68764" };

var studentResult = modelData.users.Where(q => q.UserName == u.UserName && q.Password == u.Password).FirstOrDefault();
Assert.IsTrue(studentResult == null);

var studentResult2 = modelData.users.Where(q => q.UserName == u2.UserName && q.Password == u2.Password).FirstOrDefault();
Assert.IsTrue(studentResult2 == null);
*/





