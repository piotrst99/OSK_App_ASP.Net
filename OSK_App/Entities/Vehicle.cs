using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Wpisz numer rejestracyjny")]
        [MaxLength(10, ErrorMessage = "Numer rejestracyjny musi mieć 10 znaków")]
        [Display(Name = "Numer rejestracyjny")]
        public string RegNumber { get; set; }

        [Required(ErrorMessage = "Wpisz markę pojazdu")]
        [Display(Name = "Marka")]
        public string Mark { get; set; }

        [Required(ErrorMessage = "Wpisz model pojazdu")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Przebieg")]
        public int Course { get; set; }
        
        [ForeignKey("Category")]
        [Display(Name = "Kategoria")]
        public int CategoryID { get; set; }
        
        [ForeignKey("VehicleStatus")]
        [Display(Name = "Status pojazdu")]
        public int VehicleStatID { get; set; }
        
        public virtual Category Category { get; set; }
        public virtual VehicleStatus VehicleStatus { get; set; }
    }
}
