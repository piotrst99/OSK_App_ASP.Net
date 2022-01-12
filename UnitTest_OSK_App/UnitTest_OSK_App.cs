using Microsoft.EntityFrameworkCore;
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
        //public TestDatas datas = new TestDatas();
        public ModelData data = new ModelData();

        [TestMethod]
        public void CreateLoginFromUserName_ShouldBeOk() {
            Assert.AreEqual(LoginPasswordGenerate.CreateLogin("Piotr", "Nowak", 1), "PN0001");
            Assert.AreEqual(LoginPasswordGenerate.CreateLogin("£ukasz", "Tort", 22), "LT0022");
            Assert.AreEqual(LoginPasswordGenerate.CreateLogin("Filip", "£¹cki", 143), "FL0143");
            Assert.AreEqual(LoginPasswordGenerate.CreateLogin("¯aneta", "¯arnowska", 3245), "ZZ3245");
            Assert.AreEqual(LoginPasswordGenerate.CreateLogin("Karol", "Dynia", 45754), "KD45754");
        }

        [TestMethod]
        public void LoginToPanel_ShouldBeOK() {
            //ModelData data = new ModelData();

            //var employee = data.employees.Where(q => q.User.UserName == "WC0001" && q.User.Password == "12345" && q.RoleID == 1).FirstOrDefault();
            User u = new User {ID=1, UserName = "WC0001", Password = "12345" };
            //var employeeResult = data.employees.Where(q => q.User.UserName == u.UserName && q.User.Password == "12345" && q.RoleID == 1).FirstOrDefault();
            var employeeResult = data.employees.Where(q => q.UserID == u.ID && q.RoleID == 1).FirstOrDefault();
            Assert.IsTrue(employeeResult != null);
        }

        [TestMethod]
        public void LoginToPanel_ShouldNotBeOK() {
            User u = new User { ID = 2, UserName = "KG0002", Password = "52179" };
            User u2 = new User { ID = 9, UserName = "TK0009", Password = "57913" };
            
            var employeeResult = data.employees.Where(q => q.UserID == u.ID && q.RoleID == 1).FirstOrDefault();
            Assert.IsTrue(employeeResult == null);

            var employeeResult2 = data.employees.Where(q => q.UserID == u2.ID && q.RoleID == 1).FirstOrDefault();
            Assert.IsTrue(employeeResult2 == null);
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