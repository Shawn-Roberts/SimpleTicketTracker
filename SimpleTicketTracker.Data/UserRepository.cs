using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleTicketTracker.Core.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace SimpleTicketTracker.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserRepository> _logger;
        private readonly SimpleTicketTrackerDbContext _context;

        //FIELDS

        //CONSTRUCTORS
        public UserRepository(UserManager<ApplicationUser> userManager, ILogger<UserRepository> logger, SimpleTicketTrackerDbContext context)
        {
            this._userManager = userManager;
            this._logger = logger;
            this._context = context;
        }

        //METHODS

        // ADD NEW USER TO DATABASE
        public async Task<IdentityResult> AddAsync(ApplicationUser user, string password)
        {
            _logger.LogInformation($"Adding an Application user to the database with a name of ${"user.FirstName user.LastName"}");
            var result = await _userManager.CreateAsync(user, password);
            return result;

        }

        // UPDATE USER INFORMATION
        public async Task<int> UpdateAsync(ApplicationUser user, string? FirstName, string? LastName, string? PhoneNumber, string? Email)
        {
            _logger.LogInformation($"Upating user information for user ${"user.FirstName user.LastName"}");
            var SelectedUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (FirstName != null) SelectedUser.FirstName = FirstName;
            if (LastName != null) SelectedUser.LastName = LastName;
            if (PhoneNumber != null) SelectedUser.PhoneNumber = PhoneNumber;
            if (Email != null) SelectedUser.Email = Email;
            SelectedUser.UserName = Email;
            SelectedUser.NormalizedEmail = SelectedUser.Email.ToUpper();
            SelectedUser.NormalizedUserName = SelectedUser.UserName.ToUpper();
            await _context.SaveChangesAsync();
            return await _context.SaveChangesAsync();


        }

        // DELETE USER FROM DATABASE
        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            _logger.LogInformation($"Deleting an Application user to the database with a name of ${"user.FirstName user.LastName"} and an ID of ${user.Id}");
            var result = await _userManager.DeleteAsync(user);
            return result;

        }


        // GET ALL USERS ASYNC
        public async Task<ApplicationUser[]> GetAllUsersAsync()
        {
            _logger.LogInformation($"Retriving all users from database");
            var query = _context.Users;
            return await query.ToArrayAsync();
        }

        //GET USERS BY SEARCH STRING
        public async Task<ApplicationUser[]> GetAllUsersAsync(string searchString)
        {
            _logger.LogInformation($"Getting filtered users");
            var query = _context.Users.Where(p => p.FirstName.ToUpper().Contains(searchString.ToUpper()) || p.LastName.ToUpper().Contains(searchString.ToUpper()));
            return await query.ToArrayAsync();
        }

        //GET USER BY ID
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            _logger.LogInformation($"Retriving user from database with id of {0}", userId);
            var query = _userManager.FindByIdAsync(userId);
            return await query;
        }


        //GET ALL ROLES
        public async Task<ApplicationRole[]> GetAllRolesAsync()
        {
            var query = _context.ApplicationRoles;
            return await query.ToArrayAsync();

        }

        public int GetUserCount()
        {
            var query = _context.Users.AsEnumerable().Count();
            return query;
        }


        //OVER VIEW 

    }
}
