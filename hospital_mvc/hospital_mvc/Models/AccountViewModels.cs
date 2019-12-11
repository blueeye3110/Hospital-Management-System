using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hospital_mvc.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
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

        


        [Required(ErrorMessage = "You must provide a contact number")]
        [Display(Name = "Contact No.")]
        [StringLength(10,MinimumLength = 10)]       
        public string contactNo { get; set; }

        [Display(Name = "Blood Group")]
        [DisplayFormat(NullDisplayText = "Blood Group not specified")]
        public string bloodgroup { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class RegisterStaffModel
    {
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



        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }


    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
