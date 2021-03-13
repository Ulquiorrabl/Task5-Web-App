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
            if(db.Transactions != null)
            {
                return View(db.Transactions.ToList());
            }
            else
            {
                throw new NullReferenceException("Transactions set does not exsist in current TransactionContext");
            }
        }
    }
}
