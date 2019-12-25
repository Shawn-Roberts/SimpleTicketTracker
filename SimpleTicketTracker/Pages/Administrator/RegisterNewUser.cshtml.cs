using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SimpleTicketTracker.Core.Identity;
using SimpleTicketTracker.Core.Models;
using SimpleTicketTracker.Data;

namespace SimpleTicketTracker
{
    [Authorize(Roles = "Administrator")]
    public class RegisterNewUserModel : PageModel
    {


        //FIELDS
        private readonly ILogger<RegisterNewUserModel> _logger;
        private readonly IUserRepository _userRepository;

        [BindProperty]
        public RegisterModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //CONSTRUCTORS
        public RegisterNewUserModel(IUserRepository userRepository, ILogger<RegisterNewUserModel> logger)
        {

            this._userRepository = userRepository;
            _logger = logger;
        }

        //METHODS

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };
                var result = await _userRepository.AddAsync(newUser, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    //await _signInManager.SignInAsync(newUser, isPersistent: false);
                    StatusMessage = String.Format("User {0} {1} suceessfully created", newUser.FirstName, newUser.LastName);
                    return RedirectToPage("./ManageUsers");
                }
            }
            StatusMessage = String.Format("Unable to create user");
            return RedirectToPage("./ManageUsers");
        }
    }
}