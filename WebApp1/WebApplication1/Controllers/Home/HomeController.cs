using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers.Home
{
    public class HomeController : Controller
    {

        TransactionContext db;

        public HomeController(TransactionContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            if(db.Products != null)
            {
                return View(db.Products.ToList());
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
    }
}
