using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadoHub.Data.Models;
using RadoHub.Services.Contracts;
using RadoHub.Services.Services;

namespace RadoHub.WebApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<RadoHubUser> _userManager;
        private readonly SignInManager<RadoHubUser> _signInManager;
        private readonly IUserAccountService _userAccountService; 
        public IndexModel(
            UserManager<RadoHubUser> userManager,
            SignInManager<RadoHubUser> signInManager,
            IUserAccountService userAccountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userAccountService = userAccountService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "FistName (optional)")]
            public string FirstName { get; set; }

            [Display(Name = "LastName (optional)")]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = "Phone number (optional)")]
            public string PhoneNumber { get; set; }

            [Display(Name = "City (optional)")]
            public string City { get; set; }

            [Display(Name = "Company (optional)")]
            public string Company { get; set; }
        }

        private async Task LoadAsync(RadoHubUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var city = user.City;
            var company = user.Company;

            Username = userName;

            Input = new InputModel
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                City = city,
                Company = company,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            var firstName = _userAccountService.GetFirstName(user.Id);

            if (Input.FirstName != firstName)
            {
                _userAccountService.SetFirstName(user.Id, Input.FirstName);
            }

            var lastName = _userAccountService.GetLastName(user.Id);

            if (Input.LastName != lastName)
            {
                _userAccountService.SetLastName(user.Id, Input.LastName);
            }

            var city = _userAccountService.GetUserCity(user.Id);

            if (Input.City != city)
            {
                _userAccountService.SetUserCity(user.Id, Input.City);
            }

            var company = _userAccountService.GetUserCompany(user.Id);

            if (Input.Company != company)
            {
                _userAccountService.SetUserCompany(user.Id, Input.Company);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
