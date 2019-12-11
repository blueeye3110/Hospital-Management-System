using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hospital_mvc.Models
{
    public class Room
    {
        public Room()
        {
            status = "Available";
        }

        [Key]
        [Display(Name = "Room ID")]
        public int roomId { get; set; }

        [Required(ErrorMessage = "You must provide Room Type.")]
        [Display(Name = "Room Type")]
        public string type { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }

        [Required(ErrorMessage = "You must provide Room Charges.")]
        [Display(Name = "Charges")]
        public int charge { get; set; }

        [Display(Name = "Last Used By")]
        [DisplayFormat(NullDisplayText = "Not Specified")]
        public string patientName { get; set; }

        public string caseId { get; set; }
    }
}