using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadoHub.Services.Constants;

namespace RadoHub.WebApp.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdminRole)]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
    }
}