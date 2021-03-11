using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.User
{
    public class UserController : Controller
    {
        Models.TransactionContext db;
        public UserController(Models.TransactionContext db)
        {
            this.db = db;
        } 

        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }
    }
}
