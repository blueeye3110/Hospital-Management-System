using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hospital_mvc.Models
{
    public class Report
    {
        [Key]
        public int reportId { get; set; }

        [Required(ErrorMessage = "You must provide Report Name")]
        [Display(Name = "Report Name")]
        public string fileName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Uploaded On")]
        public DateTime? date { get; set; }


        [Display(Name = "Upload Report")]
        public string filePath { get; set; }

        public int caseId { get; set; }
        public Case Case { get; set; }

        [Display(Name = "Uploaded By")]
        [DisplayFormat(NullDisplayText = "Not Specified")]
        public string uploadedBy { get; set; }

        public int staffId { get; set; }
        public Staff Staff { get; set; }

        [NotMapped]
        public HttpPostedFileBase files { get; set; }



    }
}