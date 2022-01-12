using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_API.Models
{
    public class StudentCourseData{
        public int ID { get; set; }
        public string CourseName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int ExtraHours { get; set; }
        public int SumOfPayments { get; set; }
        public string StudentCourseStatus { get; set; }
        public string Category { get; set; }
        public int CountOfPractical{ get; set; }
        public int CountOfCompletedPractical { get; set; }

    }
}
