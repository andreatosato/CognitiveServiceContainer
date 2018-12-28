using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CognitiveApp.Controllers
{
    public class CustomVisionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}