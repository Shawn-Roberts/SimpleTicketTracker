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
    public class ManageUsersModel : PageModel
    {

        //FIELDS
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserRepository> _logger;
        public IEnumerable<ApplicationUser> ApplicationUsers;

        public string CurrentFilter { get; set; }

        [TempData]
        public string StatusMessage { get; set; }


        //CONSTRUCTORS
        public ManageUsersModel(IUserRepository userRepository, ILogger<UserRepository> logger)
        {
            this._userRepository = userRepository;
            this._logger = logger;
        }


        //METHODS

        public async Task<IActionResult> OnGet(string? searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {

                _logger.LogInformation("Getting all users");
                ApplicationUsers = await _userRepository.GetAllUsersAsync();

            }
            else
            {
                _logger.LogInformation(String.Format("Getting users by search string {0}",searchString));

                ApplicationUsers = await _userRepository.GetAllUsersAsync(searchString);
            }
            return Page();

        }
    }
}