
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

namespace RadoHub.Services.Services
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
    }
}
