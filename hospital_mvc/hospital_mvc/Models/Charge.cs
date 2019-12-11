using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_mvc.Models
{
    public class Charge
    {
        [Key]
        [Display(Name = "Charge ID")]
        public int chargeID { get; set; }

        [Required(ErrorMessage = "You must provide Charge Type")]
        [Display(Name = "Type")]
        public string type { get; set; }

        [Required(ErrorMessage = "You must provide Charge Amount")]
        [Display(Name = "Amount")]
        public int amount { get; set; }
    }
}