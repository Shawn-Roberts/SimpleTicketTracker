using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleTicketTracker.Core.Identity;
using SimpleTicketTracker.Data;

namespace SimpleTicketTracker.Areas.Identity.Pages.Account.Manage
{
    public partial class ManageModel : PageModel
    {

        //FIELDS
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        [BindProperty]
        public String Username { get; set; }
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string PhoneNumber{ get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        //CONSTRUCTORS
        public ManageModel(UserManager<ApplicationUser> userManager, IUserRepository _userRepository)
        {
            this._userManager = userManager;
            this._userRepository = _userRepository;
        }

        private void LoadAsync(ApplicationUser user)
        {
            Username = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;

        }

        //METHODS
        public async Task<IActionResult> OnGetAsync(string? userId)
        {
            ApplicationUser user;
            if (userId == null)
            {
                user = await _userManager.GetUserAsync(User);
            }
            else
            {
                user = await _userManager.FindByIdAsync(userId);
            }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            LoadAsync(user);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string? userId)
        {
            ApplicationUser user;
            if (userId == null)
            {
                user = await _userManager.GetUserAsync(User);
            }
            else
            {
                user = await _userManager.FindByIdAsync(userId);
            }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (!ModelState.IsValid)
            {
                LoadAsync(user);
                StatusMessage = "Your profile has been updated";
                return Page();
            }

            var result = await _userRepository.UpdateAsync(user, FirstName, LastName, PhoneNumber, Email);

            if (result > 0)
            {
                StatusMessage = "Error Updating User";
                return Page();
            }
            StatusMessage = String.Format("Successfully updated user {0}, {1} to application",user.FirstName, user.LastName);
            return Page();



        }
    }
}
