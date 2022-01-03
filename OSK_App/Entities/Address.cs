using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class Address
    {
        [Key]
        public int ID { get; set; }
        
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        
        [Required(ErrorMessage = "Wpisz numer domu")]
        [Display(Name = "Numer domu")]
        public string NrHome { get; set; }
        
        [Display(Name = "Numer lokalu")]
        public string NrLocal { get; set; }
        
        [Required(ErrorMessage = "Wpisz nazwę miejscowości")]
        [Display(Name = "Miejscowość")]
        public string Town { get; set; }
        
        [Required(ErrorMessage = "Wpisz kod pocztowy")]
        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }
        
        [Required(ErrorMessage = "Wpisz pocztę")]
        [Display(Name = "Poczta")]
        public string Post { get; set; }
        
        [Required(ErrorMessage = "Wpisz numer telefonu")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
