using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers.Home
{
    public class HomeController : Controller
    {

        TransactionContext db;
        private readonly UserManager<Models.Authorization.AuthorizationUser> _userManager;

        public HomeController(TransactionContext db, UserManager<Models.Authorization.AuthorizationUser> userManager)
        {
            this.db = db;
            this._userManager = userManager;
        }
        [HttpPost]
        public JsonResult GetProducts(string productNameFilter, int productMaxCost)
        {
            if(productNameFilter == null && productMaxCost == 0)
            {
                return GetAllProducts();
            }
            var products = db.Products.ToList();
            if (productNameFilter != null)
            {
                products = products
                    .Where(x => x.ProductName.ToLower().Contains(productNameFilter.ToLower())).ToList();
            }
            if(productMaxCost > 0)
            {
                products = products
                    .Where(product => product.Cost <= productMaxCost).ToList();
            }
            var json = Json(new { data = products });
            return Json(new { data = products });
        }

        [HttpGet]
        public JsonResult GetAllProducts()
        {
            var products = db.Products.ToList();
            return Json(new { data = products });
        }

        [HttpGet]
        public IActionResult Index()
        {
            if(db.Products != null)
            {
                return View(db.Products.ToList()); //Json(new { data = db.Products.ToList() }) ;  //View(db.Products.ToList());
            }
            else
            {
                throw new NullReferenceException("Set of objects Products in current contexts is not exist");
            }
        }
        [HttpPost]
        public IActionResult Index(string productName)
        {
            if (db.Products != null)
            {
                var products = db.Products.Where(product => product.ProductName.Contains(productName)).ToList();
                if(products != null)
                {

                    return View(db.Products.ToList());
                }
                else
                {
                    return  RedirectToAction("Index");
                }
            }
            else
            {
                throw new NullReferenceException("Set of objects Products in current contexts is not exist");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Delete(int id)
        {
            Product pr = db.Products.FirstOrDefault(x => x.ID == id);
            if(pr != null)
            {
                db.Products.Remove(pr);
                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Product not found");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if(product != null)
            {
                if (TryValidateModel(product))
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }

            }
            else
            {
                throw new ArgumentNullException("Null object was received when trying to create Product object");
            }
            return RedirectToAction("Index");
        } 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = db.Products.FirstOrDefault(product => product.ID == id);
            if(product != null)
            {
                return View(product);
            }
            else
            {
                throw new KeyNotFoundException("Product with this id not found");
            }
        }
        [HttpPost]
        public IActionResult Update(Product updatedProduct)
        {
            var product = db.Products.FirstOrDefault(x => x.ID == updatedProduct.ID);
            if(product != null)
            {
                product.ProductName = updatedProduct.ProductName;
                product.Cost = updatedProduct.Cost;
                db.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("Null object was received when trying to update Product object");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Buy(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.ID == id);
            if (product != null) return View(product);
            else return View();

        }
        [HttpPost]
        public async Task<IActionResult> Buy(string username, int productId)
        {
            var user = await _userManager.FindByNameAsync(username);
            var product = db.Products.FirstOrDefault(prod => prod.ID == productId);
            if (user != null && product != null)
            {
                db.Transactions.Add(
                    new Models.Transaction
                    {
                        Date = DateTime.Today,
                        Cost = product.Cost,
                        Manager = db.Managers.FirstOrDefault(x => x.ManagerName == "Alex"),
                        Product = product,
                        User = user
                    }) ;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Index");
        }
    }
}
