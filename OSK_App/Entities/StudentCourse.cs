using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class StudentCourse
    {
        [Key]
        public int ID { get; set; }
        
        [ForeignKey("Course")]
        public int CourseID { get; set; }

        public string StartDate { get; set; }
        public string? EndDate { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }

        public int ExtraHours { get; set; }
        public int SumOfPayment { get; set; }

        //[ForeignKey("StudentCourseStatus")]
        //public int? StudentCourseStatID { get; set; }

        public string StudentStatus { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
        //public virtual StudentCourseStatus StudentCourseStatus { get; set; }
    }
}
