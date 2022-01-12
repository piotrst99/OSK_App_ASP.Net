using OSK_API.Models;
using OSK_App.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Models
{
    public class StudentDetails{
        [Key]
        public int ID { get; set; }

        /// PERSONAL DATA
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

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

        /// ADDRES DATA
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

        /// PAYMENT DATA
        public List<Payment> payments; 

        public Payment payment { get; set; }

        public int totalCost { get; set; }

        /// COURSES DATA
        public List<StudentCourse> studentCourses;

        /// PRACTICAL DATA
        public List<Practical> practicals;

        public List<StudentCourseData> studentDataCourses;
        ///

    }
}
