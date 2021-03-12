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

        public IActionResult Delete(int id)
        {
            var manager = db.Managers.FirstOrDefault(x => x.ManagerId == id);
            if(manager != null)
            {
                db.Managers.Remove(manager);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Models.Manager manager)
        {
            db.Managers.Add(manager);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var manager = db.Managers.FirstOrDefault(manager => manager.ManagerId == id);
            return View(manager);
        }
        [HttpPost]
        public IActionResult Update(Models.Manager updatedManager)
        {
            var manager = db.Managers.FirstOrDefault(x => x.ManagerId == updatedManager.ManagerId);
            manager.ManagerName = updatedManager.ManagerName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
