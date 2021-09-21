using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementCors.Controllers
{
    public class ClientAPI : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Charts()
        {
            return View();
        }
    }
}
