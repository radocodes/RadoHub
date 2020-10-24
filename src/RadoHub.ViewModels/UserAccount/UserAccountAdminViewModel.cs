using RadoHub.Data.Models;

namespace RadoHub.ViewModels.UserAccount
{
    public class UserAccountAdminViewModel : BaseModel<string>
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string UserRole { get; set; }
    }
}
