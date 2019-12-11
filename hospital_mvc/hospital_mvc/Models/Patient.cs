using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hospital_mvc.Models
{
    public class Patient
    {
        [Key]
        [Display(Name = "Patient ID")]
        public int patientId { get; set; }

        [Required(ErrorMessage = "You must provide a patient name.")]
        [Display(Name = "Full Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "You must provide a gender")]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required(ErrorMessage = "You must provide a date of birth")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime?  dob { get; set; }

        [Required(ErrorMessage = "You must provide a address")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "You must provide a email Id")]
        [EmailAddress]
        [Display(Name = "Email-ID")]
        public string emailId { get; set; }

        [Required(ErrorMessage = "You must provide a password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "You must provide a contact number")]
        [Display(Name = "Contact No.")]
        [StringLength(10, MinimumLength = 10)]
        public string contactNo { get; set; }

        [Display(Name = "Blood Group")]
        [DisplayFormat(NullDisplayText = "Blood Group not specified")]
        public string bloodgroup { get; set; }

        
        
        public ICollection<Case> cases { get; set; }

        

    }
}