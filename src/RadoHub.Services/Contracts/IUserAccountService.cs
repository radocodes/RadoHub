using Microsoft.AspNetCore.Identity;
using RadoHub.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadoHub.Services.Contracts
{
    public interface IUserAccountService
    {
        public IEnumerable<RadoHubUser> GetAllUsers();

        RadoHubUser GetUserById(string userId);

        string GetFirstName(string userId);
        
        string GetLastName(string userId);
        
        string GetUserCity(string userId);

        string GetUserCompany(string userId);

        void SetFirstName(string userId, string firstName);

        void SetLastName(string userId, string lastName);

        void SetUserCity(string userId, string city);

        void SetUserCompany(string userId, string company);

        public Task<IdentityResult> DeleteUserAsync(RadoHubUser user);

        public Task<IEnumerable<string>> GetUserRolesAsync(string userId);

        public Task<IdentityResult> AddUserInRoleAsync(RadoHubUser user, string role);

        public Task<IdentityResult> RemoveUserFromRoleAsync(RadoHubUser user, string role);

        public string GetUserStrongestRole(string userId);
    }
}
