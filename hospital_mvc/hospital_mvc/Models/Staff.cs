using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_mvc.Models
{
    public class Staff
    {
        [Key]
        [Display(Name = "Staff ID")]
        public int staffId { get; set; }

        [Required(ErrorMessage = "You must provide a patient name.")]
        [Display(Name = "Full Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "You must provide a gender")]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required(ErrorMessage = "You must provide a date of birth")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime? dob { get; set; }

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

        [Required(ErrorMessage = "You must provide a Salary.")]
        [Display(Name = "Salary")]
        public int salary { get; set; }

        [Required(ErrorMessage = "You must provide Appointment Charge")]
        [Display(Name = "Appointment Charge")]
        public int appcharge { get; set; }

        [Required(ErrorMessage = "You must provide Department details.")]
        [Display(Name = "Department")]
        public string dept { get; set; }

        [Required(ErrorMessage = "You must provide Designation details")]
        [Display(Name = "Designation")]
        public string type { get; set; }

        [Display(Name = "Blood Group")]
        [DisplayFormat(NullDisplayText = "Blood Group not specified")]
        public string bloodgroup { get; set; }

        [Display(Name = "Description")]
        public string desc { get; set; }

        public ICollection<Appointment> appointments { get; set; }
        public ICollection<Staff> staffs { get; set; }

        

    }
}