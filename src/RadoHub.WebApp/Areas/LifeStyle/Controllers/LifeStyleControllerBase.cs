using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RadoHub.WebApp.Areas.LifeStyle.Controllers
{
    public class LifeStyleControllerBase : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}