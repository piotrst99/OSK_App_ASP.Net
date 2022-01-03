using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class Employee
    {
        
        [Key]
        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        
        //[Required(ErrorMessage = "Wpisz numer instruktora")]
        [StringLength(10, ErrorMessage = "Numer instruktora jest krótka", MinimumLength = 6)]
        [Display(Name = "Numer instruktora")]
        public string NrInstructor { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
