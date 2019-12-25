
using Microsoft.AspNetCore.Identity;
using SimpleTicketTracker.Core.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTicketTracker.Data
{
    public interface IUserRepository
    {
        //GET 
        Task<ApplicationUser[]> GetAllUsersAsync();
        Task<ApplicationUser[]> GetAllUsersAsync(string searchString);
        Task<ApplicationUser> GetUserByIdAsync(string userId);

        Task<ApplicationRole[]> GetAllRolesAsync();


        //ADD USERS
        Task<IdentityResult> AddAsync(ApplicationUser user, string password);

        //UPDATE USERS
        Task<int> UpdateAsync(ApplicationUser user, string? FirstName, string? LastName, string? PhoneNumber, string? Email);

        //DELETE USER
        Task<IdentityResult> DeleteAsync(ApplicationUser user);

        //OVERVIEW STATS
        int GetUserCount();
    }
}
