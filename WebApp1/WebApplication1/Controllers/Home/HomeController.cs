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
            return View(db.Products.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Delete(int id)
        {
            Product pr = db.Products.FirstOrDefault(x => x.ID == id);
            db.Products.Remove(pr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        } 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            Product pr = db.Products.FirstOrDefault(product => product.ID == id);
            return View(pr);
        }
    }
}
