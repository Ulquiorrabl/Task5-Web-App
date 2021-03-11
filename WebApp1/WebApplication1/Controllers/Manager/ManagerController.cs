using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.Manager
{
    public class ManagerController : Controller
    {
        Models.TransactionContext db;

        public ManagerController(Models.TransactionContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.Managers.ToList());
        }
    }
}
