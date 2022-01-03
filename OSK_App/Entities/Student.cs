using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class Student
    {
        [Key]
        [ForeignKey("User")]
        public int UserID { get; set; }
        
        public virtual User User { get; set; }

        //public virtual Practical Practical { get; set; }
    }
}
