using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hospital_mvc.Models
{
    public class Appointment
    {
        public Appointment()
        {
            status = "Not Conducted";
        }
        [Key]
        [Display(Name = "Appointment ID")]
        public int appID { get; set; }

        [Required(ErrorMessage = "You must provide Appointment Date")]
        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        public DateTime? date { get; set; }

        [Display(Name = "Diagnosis Details")]
        [DisplayFormat(NullDisplayText = "Not Specified")]
        public string details { get; set; }

        [Display(Name = "Prescription")]
        [DisplayFormat(NullDisplayText = "Not Specified")]
        public string pres { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }

        public int caseId { get; set; }
        public Case Case { get; set; }

        [Required(ErrorMessage = "You must provide patient name")]
        [Display(Name = "Patient Name")]
        public string patientname { get; set; }
        
        public int staffId { get; set; }
        public Staff Staff { get; set; }

    }
}