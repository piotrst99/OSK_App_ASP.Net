using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Kategoria")]
        public string Symbol { get; set; }
    }
}
