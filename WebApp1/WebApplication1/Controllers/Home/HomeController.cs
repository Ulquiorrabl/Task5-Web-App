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
            if(db.Products != null)
            {
                    var products = db.Products.ToList();
                    return Json(new { data = products });
            }
            else
            {
                throw new NullReferenceException("Set of objects Products in current contexts is not exist");
            }

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
           /* if(db.Products != null)
            {
                return View(db.Products.ToList()); //Json(new { data = db.Products.ToList() }) ;  //View(db.Products.ToList());
            }
            else
            {
                throw new NullReferenceException("Set of objects Products in current contexts is not exist");
            } */
        }
        /*[HttpPost]
        public IActionResult Index(string productName)
        {
            productName = null;
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
        } */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Delete(int id)
        {
              var pr = db.Products.FirstOrDefault(x => x.ID == id);
                if (pr != null)
                {
                    try
                    {
                        db.Products.Remove(pr);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Error {e.Message}. Stack trace: {e.StackTrace}");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Product not found");
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
                    try
                    {
                        db.Products.Add(product);
                        db.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine($"Error {e.Message}. Stack trace: {e.StackTrace}");
                    }

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
            if (db.Products != null)
            {
                var product = db.Products.FirstOrDefault(product => product.ID == id);
                if (product != null)
                {
                    return View(product);
                }
                else
                {
                    throw new KeyNotFoundException("Product with this id not found");
                }
            }
            else
            {
                throw new KeyNotFoundException("Products set is not exist in current database context");
            }
        }
        [HttpPost]
        public IActionResult Update(Product updatedProduct)
        {
            if(db.Products != null)
            {
                var product = db.Products.FirstOrDefault(x => x.ID == updatedProduct.ID);
                if (product != null)
                {
                    if (TryValidateModel(updatedProduct))
                    {
                        try
                        {
                            product.ProductName = updatedProduct.ProductName;
                            product.Cost = updatedProduct.Cost;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine($"Error {e.Message}. Stack trace: {e.StackTrace}");
                        }
                    }

                }
                else
                {
                    throw new ArgumentNullException("Null object was received when trying to update Product object");
                }
            }
            else
            {
                throw new KeyNotFoundException("Products set is not exist in current database context");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Buy(int id)
        {
            if(id > 0)
            {
                if(db.Products != null)
                {
                    var product = db.Products.FirstOrDefault(x => x.ID == id);
                    if (product != null) return View(product);
                    else return View();
                }
                else
                {
                    throw new KeyNotFoundException("Products set is not exist in current database context");
                }
            }
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
                        Date = DateTime.Now,
                        Cost = product.Cost,
                        Manager = db.Managers.FirstOrDefault(),
                        ProductId = product.ID,
                        UserId = user.Id,
                    }) ;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Index");
        }
    }
}
