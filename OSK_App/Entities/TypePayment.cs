using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class TypePayment{
        [Key]
        public int ID { get; set; }
        
        [Display(Name = "Rodzaj wpłaty")]
        public string Name { get; set; }
    }
}
