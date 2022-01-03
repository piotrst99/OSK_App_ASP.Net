using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App.Entities
{
    public class PaymentStatus
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Status wpłaty")]
        public string Name { get; set; }
    }
}
