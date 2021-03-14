using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers.Manager
{
    [Authorize (Roles = "admin")]
    public class ManagerController : Controller
    {
        Models.TransactionContext db;

        public ManagerController(Models.TransactionContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            if(db.Managers != null)
            {
                return View(db.Managers.ToList());
            }
            else
            {
                throw new NullReferenceException("Managers set does not exist in current TransactionContext");
            }
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
            if(manager != null)
            {
                if (TryValidateModel(manager))
                {
                    db.Managers.Add(manager);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var manager = db.Managers.FirstOrDefault(manager => manager.ManagerId == id);
            if (manager != null) return View(manager);
            else return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(Models.Manager updatedManager)
        {
            var manager = db.Managers.FirstOrDefault(x => x.ManagerId == updatedManager.ManagerId);
            if(manager != null)
            {
                if (TryValidateModel(updatedManager))
                {
                    manager.ManagerName = updatedManager.ManagerName;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
