using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RadoHub.WebApp.Areas.LifeStyle.Controllers.Cooking
{
    public class CookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}