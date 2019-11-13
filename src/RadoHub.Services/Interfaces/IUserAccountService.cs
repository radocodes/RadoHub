using Microsoft.AspNetCore.Identity;
using RadoHub.Data.Models;
using System.Threading.Tasks;

namespace RadoHub.Services.Interfaces
{
    public interface IUserAccountService
    {
        string GetFirstName(string userId);
        
        string GetLastName(string userId);
        
        string GetUserCity(string userId);

        string GetUserCompany(string userId);

        void SetFirstName(string userId, string firstName);

        void SetLastName(string userId, string lastName);

        void SetUserCity(string userId, string city);

        void SetUserCompany(string userId, string company);
    }
}
