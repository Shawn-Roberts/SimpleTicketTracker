using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace SimpleTicketTracker.Core.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required, StringLength(60, MinimumLength = 3),]
        [Display(Name = "First Name", Prompt = "Please enter a user first name")]
        public string FirstName { get; set; }

        [Required, StringLength(60, MinimumLength = 3),]
        [Display(Name = "Last Name", Prompt = "Please enter a user last name")]
        public string LastName { get; set; }


    }
}
