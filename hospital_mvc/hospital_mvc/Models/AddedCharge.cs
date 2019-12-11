using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_mvc.Models
{
    public class AddedCharge
    {
        [Key]
        public int addedchargeId { get; set; }

        [Display(Name = "Charge Type")]
        public string type { get; set; }

        [Display(Name = "Charge Amount")]
        public int amount { get; set; }

        public int caseId { get; set; }
        public Case Case { get; set; }
    }
}