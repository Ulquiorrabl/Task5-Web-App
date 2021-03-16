using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.Error
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound(int id)
        {
            return View(id);
        }
    }
}
