using Microsoft.AspNetCore.Mvc;

namespace RadoHub.WebApp.Areas.Administration.Controllers
{
    public class HomeController : AdministrationControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}