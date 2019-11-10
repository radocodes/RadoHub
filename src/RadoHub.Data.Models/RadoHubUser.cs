using Microsoft.AspNetCore.Identity;

namespace RadoHub.Data.Models
{
    public class RadoHubUser : IdentityUser
    {
        public RadoHubUser()
        {

        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Company { get; set; }
    }
}
