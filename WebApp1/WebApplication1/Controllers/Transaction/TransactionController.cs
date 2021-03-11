using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.Transaction
{
    public class TransactionController : Controller
    {
        Models.TransactionContext db;

        public TransactionController(Models.TransactionContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.Transactions.ToList());
        }
    }
}
