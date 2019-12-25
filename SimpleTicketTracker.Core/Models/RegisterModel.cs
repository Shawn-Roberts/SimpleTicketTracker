using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleTicketTracker.Core.Models
{
    public class RegisterModel
    {
        [Required, StringLength(60, MinimumLength = 3),]
        [Display(Name = "First Name", Prompt = "Please enter a user first name")]
        public string FirstName { get; set; }

        [Required, StringLength(60, MinimumLength = 3),]
        [Display(Name = "Last Name", Prompt = "Please enter a user last name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Please enter an email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Please enter a password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password", Prompt = "Please confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
