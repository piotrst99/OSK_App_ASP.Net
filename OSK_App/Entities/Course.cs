using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Wpisz nazwę kursu")]
        public string Name { get; set; }
        
        [Display(Name = "Il. teorii")]
        [Required(ErrorMessage = "Podaj liczbę godzin zajęć teoretycznych")]
        public int AmountTheoretical { get; set; }

        [Display(Name = "Il. praktyki")]
        [Required(ErrorMessage = "Podaj liczbę godzin zajęć praktycznych")]
        public int AmountPractical { get; set; }
        
        [ForeignKey("Category")]
        [Display(Name = "Kategoria")]
        public int CategoryID { get; set; }

        [Display(Name = "Koszt kursu")]
        [Required(ErrorMessage = "Podaj koszt kursu")]
        [Column(TypeName = "decimal(8, 2)")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public Decimal CourseCost { get; set; }

        [Display(Name = "Koszt zajęć praktycznych")]
        public int? ExtraPracticalCost { get; set; }

        [Display(Name = "Ilość kursantów")]
        public int CountOfStudents { get; set; }

        public virtual Category Category { get; set; }
    }
}
