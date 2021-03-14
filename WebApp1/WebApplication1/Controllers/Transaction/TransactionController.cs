using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers.Transaction
{
    [Authorize(Roles = "admin")]
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
                List<Models.Transaction> transactions = db.Transactions.ToList();
                return View(transactions);
            }
            else
            {
                throw new NullReferenceException("Transactions set does not exsist in current TransactionContext");
            }
        }
    }
}
