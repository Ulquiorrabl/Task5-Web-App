using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers.Transaction
{
    [Authorize(Roles = "admin")]
    public class TransactionController : Controller
    {
        Models.TransactionContext db;
        UserManager<Models.Authorization.AuthorizationUser> _userManager;

        public TransactionController(Models.TransactionContext db, UserManager<Models.Authorization.AuthorizationUser> userManager)
        {
            this.db = db;
            this._userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllTransactions()
        {
            if (db.Transactions != null)
            {
                var transactions = await GetNormilizedTransactions();
                var result = Json(new { data = transactions});
                return result;
            }
            else
            {
                throw new NullReferenceException("Transactions set does not exsist in current TransactionContext");
            }
        }

        private async Task<List<Models.Transaction>> GetNormilizedTransactions()
        {
            if (db.Transactions != null)
            {
                var transactions = db.Transactions.ToList();
                foreach (var transaction in transactions)
                {
                    transaction.Product = db.Products.FirstOrDefault(product => product.ID == transaction.ProductId);
                    transaction.User = await _userManager.FindByIdAsync(transaction.UserId);
                }
                return transactions;
            }
            else
            {
                throw new NullReferenceException("Transactions set does not exsist in current TransactionContext");
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetTransactions(string userNameFilter, string productNameFilter)
        {
            if(userNameFilter == null && productNameFilter == null)
            {
                return await GetAllTransactions();
            }
            var transactions = await GetNormilizedTransactions();
            if(userNameFilter != null)
            {
                transactions = transactions
                    .Where(transaction => transaction.User.UserName.ToLower().Contains(userNameFilter.ToLower())).ToList();
            }
            if(productNameFilter != null)
            {
                transactions = transactions
                    .Where(transaction => transaction.Product.ProductName.ToLower().Contains(productNameFilter.ToLower())).ToList();
            }
            return Json(new { data = transactions });
        }
        public IActionResult Delete(int Id)
        {
            var transaction = db.Transactions.First(transaction => transaction.TransactionId == Id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
