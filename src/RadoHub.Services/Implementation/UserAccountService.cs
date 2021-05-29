
using Microsoft.AspNetCore.Identity;
using RadoHub.Data.Models;
using RadoHub.Services.Contracts;
using RadoHub.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using RadoHub.Services.Constants;

namespace RadoHub.Services.Implementation
{
    public class UserAccountService : IUserAccountService
    {
        private readonly RadoHubDbContext DbContext;
        private readonly UserManager<RadoHubUser> UserManager;

        public UserAccountService(RadoHubDbContext dbContext, UserManager<RadoHubUser> userManager)
        {
            this.DbContext = dbContext;
            this.UserManager = userManager;
        }

        public IEnumerable<RadoHubUser> GetAllUsers()
        {
            var users = DbContext.Users.ToList<RadoHubUser>();

            return users;
        }

        public RadoHubUser GetUserById(string userId)
        {
            var user = this.UserManager
                .FindByIdAsync(userId)
                .GetAwaiter().GetResult();

            return user;
        }

        public string GetFirstName(string userId)
        {
            var firstName = this.UserManager
                .FindByIdAsync(userId)
                .GetAwaiter().GetResult()
                .FirstName;

            return firstName;
        }

        public string GetLastName(string userId)
        {
            var lastName = this.UserManager
                .FindByIdAsync(userId)
                .GetAwaiter().GetResult()
                .LastName;

            return lastName;
        }

        public string GetUserCity(string userId)
        {
            var city = this.UserManager
                .FindByIdAsync(userId)
                .GetAwaiter().GetResult()
                .City;

            return city;
        }

        public string GetUserCompany(string userId)
        {
            var company = this.UserManager
                .FindByIdAsync(userId)
                .GetAwaiter().GetResult()
                .Company;

            return company;
        }

        public void SetFirstName(string userId, string firstName)
        {
            this.UserManager
                .FindByIdAsync(userId)
                .GetAwaiter().GetResult()
                .FirstName = firstName;

            this.DbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void SetLastName(string userId, string lastName)
        {
            this.UserManager
                .FindByIdAsync(userId)
                .GetAwaiter().GetResult()
                .LastName = lastName;

            this.DbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void SetUserCity(string userId, string city)
        {
            this.UserManager
                .FindByIdAsync(userId)
                .GetAwaiter().GetResult()
                .City = city;

            this.DbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void SetUserCompany(string userId, string company)
        {
            this.UserManager
                .FindByIdAsync(userId)
                .GetAwaiter().GetResult()
                .Company = company;

            this.DbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public async Task<IdentityResult> DeleteUserAsync(RadoHubUser user)
        {
            await this.RemoveUserFromRoleAsync(user, UserRoles.AdminRole);
            return await this.UserManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            RadoHubUser user = this.GetUserById(userId);
            var users = await this.UserManager.GetRolesAsync(user);

            return users;
        }

        public async Task<IdentityResult> AddUserInRoleAsync(RadoHubUser user, string role)
        {
            return await this.UserManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(RadoHubUser user, string role)
        {
            return await this.UserManager.RemoveFromRoleAsync(user, role);
        }

        public string GetUserStrongestRole(string userId)
        {
            IEnumerable<string> userRoles = this.GetUserRolesAsync(userId).GetAwaiter().GetResult();

            string userStrongestRole = UserRoles.DefaultUserRole;
            foreach (var role in userRoles)
            {
                if (role == UserRoles.AdminRole)
                {
                    userStrongestRole = role;

                    return userStrongestRole;
                }
            }

            return userStrongestRole;
        }
    }
}
