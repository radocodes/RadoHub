using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RadoHub.Data.Models;
using RadoHub.Services.Constants;
using RadoHub.Services.Contracts;
using RadoHub.ViewModels.UserAccount;
using System.Collections.Generic;

namespace RadoHub.WebApp.Areas.Administration.Controllers
{
    public class UserAccountController : AdministrationControllerBase
    {
        private readonly IUserAccountService userAccountService;
        private readonly IMapper mapper;

        public UserAccountController(IUserAccountService userAccountService, IMapper mapper)
        {
            this.userAccountService = userAccountService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            if (TempData["statusMessage"] != null)
            {
                ViewData["statusMessage"] = TempData["statusMessage"];
            }

            var viewModel = new AllUserAccountsViewModel();

            IEnumerable<RadoHubUser> users = this.userAccountService.GetAllUsers();

            viewModel.UserAccounts = this.mapper.Map<IEnumerable<UserAccountAdminViewModel>>(users);

            foreach (var user in viewModel.UserAccounts)
            {
                user.UserRole = this.userAccountService.GetUserStrongestRole(user.Id);
            }

            return this.View(viewModel);
        }

        public IActionResult UserDetails(string id)
        {
            RadoHubUser user = this.userAccountService.GetUserById(id);

            UserAccountAdminViewModel viewModel = mapper.Map<UserAccountAdminViewModel>(user);
            viewModel.UserRole = this.userAccountService.GetUserStrongestRole(user.Id);

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult MakeUserAdmin(string id)
        {
            RadoHubUser user = this.userAccountService.GetUserById(id);
            var operationSucceeded = this.userAccountService.AddUserInRoleAsync(user, UserRoles.AdminRole).GetAwaiter().GetResult().Succeeded;

            if (operationSucceeded)
            {
                TempData["statusMessage"] = $"User \"{user.UserName}\" was added to Admin role successfully!";
                return RedirectToAction("Index", "UserAccount");
            }
            else
            {
                TempData["statusMessage"] = $"Action Failed! | Operation succeeded";
                return RedirectToAction("Index", "UserAccount");
            }
        }

        [HttpPost]
        public IActionResult RemoveUserFromAdminRole(string id)
        {
            RadoHubUser user = this.userAccountService.GetUserById(id);
            var operationSucceeded = this.userAccountService.RemoveUserFromRoleAsync(user, UserRoles.AdminRole).GetAwaiter().GetResult().Succeeded;

            if (operationSucceeded)
            {
                TempData["statusMessage"] = $"User \"{user.UserName}\" was removed from Admin role successfully!";
                return RedirectToAction("Index", "UserAccount");
            }
            else
            {
                TempData["statusMessage"] = $"Action Failed! | Operation succeeded";
                return RedirectToAction("Index", "UserAccount");
            }
        }

        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            RadoHubUser user = this.userAccountService.GetUserById(id);
            var operationSucceeded = this.userAccountService.DeleteUserAsync(user).GetAwaiter().GetResult().Succeeded;

            if (operationSucceeded)
            {
                TempData["statusMessage"] = $"User \"{user.UserName}\" was Deleted successfully!";
                return RedirectToAction("Index", "UserAccount");
            }
            else
            {
                TempData["statusMessage"] = $"Action Failed! | Operation succeeded";
                return RedirectToAction("Index", "UserAccount");
            }
        }
    }
}
