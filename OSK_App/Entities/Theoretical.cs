using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class Theoretical
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }

        public string Data { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Topic { get; set; }
        public string Room { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Course Course { get; set; }
        public virtual Category Category { get; set; }
    }
}
