using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SimpleTicketTracker.Core.Identity;
using SimpleTicketTracker.Data;

namespace SimpleTicketTracker
{
    [Authorize(Roles = "Administrator")]
    public class DeleteUserModel : PageModel
    {

        //FIELDS
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RegisterNewUserModel> _logger;

        public ApplicationUser ApplicationUser { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //CONSTRUCTORS
        public DeleteUserModel(IUserRepository userRepository, ILogger<RegisterNewUserModel> logger)
        {
            this._userRepository = userRepository;
            this._logger = logger;
        }

        //METHODS

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            ApplicationUser = await _userRepository.GetUserByIdAsync(userId);
            if (ApplicationUser == null)
            {
                return RedirectToPage("../Error");
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string userId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //stopping testers from removing necessary accounts
            if (userId == "1" || userId == "2")
            {
                _logger.LogInformation("User unsuccessfully deleted");
                StatusMessage = String.Format("You are unable to delete this account in the demo");
                return RedirectToPage("./ManageUsers");
            }
            ApplicationUser = await _userRepository.GetUserByIdAsync(userId);

            if (ApplicationUser != null)
            {
                var deleteResult = _userRepository.DeleteAsync(ApplicationUser);
                if (deleteResult.Result.Succeeded)
                {
                    _logger.LogInformation("User unsuccessfully deleted");
                    StatusMessage = String.Format("User {0} {1} suceessfully deleted", ApplicationUser.FirstName, ApplicationUser.LastName);
                    return RedirectToPage("./ManageUsers");
                }
            }
            _logger.LogInformation("User unsuccessfully deleted");
            StatusMessage = String.Format("Error deleting user {0} {1}", ApplicationUser.FirstName, ApplicationUser.LastName);
            return RedirectToPage("./ManageUsers");

        }
    }


}