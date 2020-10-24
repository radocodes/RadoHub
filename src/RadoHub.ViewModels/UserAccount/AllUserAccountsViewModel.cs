using System.Collections.Generic;

namespace RadoHub.ViewModels.UserAccount
{
    public class AllUserAccountsViewModel
    {
        public IEnumerable<UserAccountAdminViewModel> UserAccounts { get; set; }
    }
}
