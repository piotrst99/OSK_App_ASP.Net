using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_OSK_App.TestsData
{
    public class TestDatas{
        public List<OSK_App.Entities.User> users = new List<OSK_App.Entities.User>();
        public List<OSK_App.Entities.Employee> employees = new List<OSK_App.Entities.Employee>();
        public List<OSK_App.Entities.Student> students = new List<OSK_App.Entities.Student>();

        public TestDatas() {
            users.Add(new OSK_App.Entities.User() { ID = 1, UserName = "WC0001", Password = "12345" });
            users.Add(new OSK_App.Entities.User() { ID = 2, UserName = "KG0002", Password = "52179" });
            users.Add(new OSK_App.Entities.User() { ID = 3, UserName = "JA0003", Password = "68764" });
            users.Add(new OSK_App.Entities.User() { ID = 9, UserName = "TK0009", Password = "57913" });
            users.Add(new OSK_App.Entities.User() { ID = 26, UserName = "AS0026", Password = "38606" });

            employees.Add(new OSK_App.Entities.Employee() { UserID = 1, RoleID = 1, NrInstructor = null });
            employees.Add(new OSK_App.Entities.Employee() { UserID = 2, RoleID = 2, NrInstructor = "XYZ0001" });
            employees.Add(new OSK_App.Entities.Employee() { UserID = 3, RoleID = 2, NrInstructor = "XYZ0022" });

            students.Add(new OSK_App.Entities.Student() { UserID = 9 });
            students.Add(new OSK_App.Entities.Student() { UserID = 26 });

        }

        public int LoginDataIsCorrect(string login, string password) {
            var employee = employees.Where(q => q.User.UserName == login && q.User.Password == password && q.RoleID == 1).FirstOrDefault();
            return employee != null ? 1 : 0;
        }

    }
}
