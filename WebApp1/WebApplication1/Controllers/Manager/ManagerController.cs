using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

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
        [HttpGet]
        public JsonResult GetAllManagers()
        {
            if(db.Managers != null)
            {
                return Json(new { data = db.Managers.ToList() });
            }
            else
            {
                throw new NullReferenceException("Context does not contain set for Managers");
            }
        }

        [HttpPost]
        public JsonResult GetManagers(string nameFilter, int idFilter)
        {
            if(db.Managers != null)
            {
                if (nameFilter == null && idFilter == 0)
                {
                    return GetAllManagers();
                }
                var managers = db.Managers.ToList();
                if (idFilter != 0)
                {
                    managers = managers.Where(manager => manager.ManagerId == idFilter).ToList();
                }
                if (nameFilter != null)
                {
                    managers = managers
                        .Where(manager => manager.ManagerName.ToLower().Contains(nameFilter.ToLower())).ToList();
                }
                return Json(new { data = managers });
            }
            else
            {
                throw new NullReferenceException("Context does not contain set for Managers");
            }
 
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            var manager = db.Managers.FirstOrDefault(x => x.ManagerId == id);
            if(manager != null)
            {
                try
                {
                    db.Managers.Remove(manager);
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    Debug.WriteLine($"Error {e.Message}. Stack trace: {e.StackTrace}");
                }
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
                    try
                    {
                        db.Managers.Add(manager);
                        db.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine($"Error {e.Message}. Stack trace: {e.StackTrace}");
                    }

                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            if(db.Managers != null)
            {
                var manager = db.Managers.FirstOrDefault(manager => manager.ManagerId == id);
                if (manager != null) return View(manager);
                else return RedirectToAction("Index");
            }
            else
            {
                throw new NullReferenceException("Context does not contain set for Managers");
            }

        }
        [HttpPost]
        public IActionResult Update(Models.Manager updatedManager)
        {
            if (db.Managers != null)
            {
                var manager = db.Managers.FirstOrDefault(x => x.ManagerId == updatedManager.ManagerId);
                if (manager != null)
                {
                    if (TryValidateModel(updatedManager))
                    {
                        manager.ManagerName = updatedManager.ManagerName;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                throw new NullReferenceException("Context does not contain set for Managers");
            }

        }
    }
}
