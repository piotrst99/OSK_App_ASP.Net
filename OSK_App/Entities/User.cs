using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Display(Name = "Hasło")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Wpisz pierwsze imię")]
        [Display(Name = "Pierwsze imię")]
        public string FirstName { get; set; }
        
        [Display(Name = "Drugie imię")]
        public string SecondName { get; set; }
        
        [Required(ErrorMessage = "Wpisz nazwisko")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Wpisz numer PESEL")]
        [MinLength(11, ErrorMessage = "Numer PESEL ma 11 znaków")]
        [MaxLength(11, ErrorMessage = "Numer PESEL ma 11 znaków")]
        [Display(Name = "PESEL")]
        public string PESEL { get; set; }
        
        [Display(Name = "Data urodzenia")]
        public string DateOfBirth { get; set; }
        
        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public virtual Address Address{ get; set; }
        
        public virtual Student Student { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
