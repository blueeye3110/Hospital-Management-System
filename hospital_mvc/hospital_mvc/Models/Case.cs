using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_mvc.Models
{
    public class Case
    {
        public Case()
        {
            status = "open";
            bill = 0;
            paid = 0;
            due = 0;
        }

        [Key]
        [Display(Name = "Case ID")]
        public int caseId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Opened On")]
        [DisplayFormat(NullDisplayText = "Not Specified",ApplyFormatInEditMode = true,DataFormatString = "0:yyyy/MM/dd")]
        public DateTime? opendate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Closed On")]
        [DisplayFormat(NullDisplayText = "Not Specified")]
        public DateTime? closedate { get; set; }


        [Display(Name = "Total Bill")]
        public int bill { get; set; }

        [Display(Name = "Paid Amount")]
        public int paid { get; set; }

        [Display(Name = "Due Amount")]
        public int due { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }

        public string roombooking { get; set; }

        public int patientId { get; set; }
        public Patient Patient { get; set; }

        public ICollection<Appointment> appointments { get; set; }
        public ICollection<Report> reports { get; set; }
        public ICollection<AddedCharge> addedCharges { get; set; }


    }
}