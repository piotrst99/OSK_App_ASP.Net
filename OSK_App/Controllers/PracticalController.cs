using Microsoft.AspNetCore.Mvc;
using OSK_App.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OSK_App.Entities;
using System.Globalization;

namespace OSK_App.Controllers
{
    [Route("/PratcicalCourse")]
    public class PracticalController : Controller
    {
        private readonly ApplicationContext context;

        public PracticalController(ApplicationContext context) {
            this.context = context;
        }

        public struct EditPracticalData{
            public string endTime { get; set; }
            public string category { get; set; }
            public int course { get; set; }
            public int statusID { get; set; }
            public int practicalID { get; set; }
        }

        [Route("GetCategiriesForPracticalDetails")]
        public IActionResult GetCategiriesForPracticalDetails(int[] tab) {

            List<string> categoryTab = new List<string>();

            for (int i = 0; i < tab.Length; i++) {
                var studentID = context.practicals.Where(q => q.ID == tab[i]).Select(q => q.StudentID).FirstOrDefault();
                var studentCourseID = context.studentCourses.Where(q => q.StudentID == studentID).Select(q => q.CourseID).FirstOrDefault();
                var course = context.courses.Where(q => q.ID == studentCourseID).FirstOrDefault();
                var category = context.categories.Where(q => q.ID == course.CategoryID).FirstOrDefault();
                categoryTab.Add(category.Symbol);
            }

            return Json(new { result = categoryTab });
        }

        [Route("SavePratcialData")]
        //public IActionResult SavePratcialData(int practcicalID, string endTime, int course, int practicalStatusID, string category) {
        public IActionResult SavePratcialData(string practicalJsonData) {
            var data = JsonConvert.DeserializeObject<EditPracticalData>(practicalJsonData);

            var practical = context.practicals.Where(q => q.ID == data.practicalID).FirstOrDefault();

            practical.EndTime = data.endTime;
            practical.Category = data.category;
            practical.Course = data.course;
            practical.PracticalStatID = data.statusID;

            context.SaveChanges();
            
            return Json(new { result = true });
        }

        [Route("GetPracticalForEmployee")]
        public IActionResult GetPracticalForEmployee(int id, string beginDate, string endDate) {

            var employeePracticals = new List<Practical>();

            try {
                
                if(!String.IsNullOrEmpty(beginDate) && !string.IsNullOrEmpty(endDate)){ 
                    if(beginDate == endDate) {
                        employeePracticals = context.practicals.Where(q => q.EmployeeID == id && q.Data == beginDate).ToList();
                    }
                    else {
                        employeePracticals = context.practicals.Where(q => q.EmployeeID == id).ToList().Where(q=>
                            (DateTime.ParseExact(q.Data, "yyyy-MM-dd", CultureInfo.InvariantCulture) >= DateTime.ParseExact(beginDate,"yyyy-MM-dd", CultureInfo.InvariantCulture)) &&
                            (DateTime.ParseExact(q.Data, "yyyy-MM-dd", CultureInfo.InvariantCulture) < DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture))).ToList();
                    }

                }
                else {
                    employeePracticals = context.practicals.Where(q => q.EmployeeID == id).ToList();
                }


                foreach (var ePractical in employeePracticals) {
                    var student = context.students.Where(q => q.UserID == ePractical.StudentID).FirstOrDefault();
                    var studentUser = context.users.Where(q => q.ID == student.UserID).Select(d => new User { FirstName = d.FirstName, Surname = d.Surname }).FirstOrDefault();
                    //var studentUser = context.users.Where(q => q.ID == student.UserID).FirstOrDefault();//.Select(d=>new User{ FirstName=d.FirstName, Surname=d.Surname }).FirstOrDefault();
                    student.User = studentUser;
                    var practicalStatus = context.practicalStatuses.Where(q => q.ID == ePractical.PracticalStatID).FirstOrDefault();
                }

                return Json(new { result = employeePracticals });

            }
            catch (Exception e) {
                return Json(new { result = e.ToString() });
            }

        }

        private string ConvertDateFormat(string date){
            return date.Substring(8, 2) + "." + date.Substring(5, 2) + "." + date.Substring(0, 4);
            // yyyy-MM-dd
            // dd.MM.yyyy
        }

    }
}

