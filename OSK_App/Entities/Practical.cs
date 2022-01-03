using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class Practical
    {
        [Key]
        public int ID { get; set; }
        
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        
        [ForeignKey("Vehicle")]
        public int? VehicleID { get; set; }
        public string Data { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? Course { get; set; }
        
        [ForeignKey("PracticalStatus")]
        public int PracticalStatID { get; set; }

        public string Category { get; set; }
        public bool IsCancel { get; set; }

        public virtual Student Student { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual PracticalStatus PracticalStatus { get; set; }
    }
}
