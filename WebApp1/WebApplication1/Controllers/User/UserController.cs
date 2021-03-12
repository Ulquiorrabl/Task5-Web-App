using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.User
{
    public class UserController : Controller
    {
        Models.TransactionContext db;
        public UserController(Models.TransactionContext db)
        {
            this.db = db;
        } 

        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public IActionResult Delete(int id)
        {
            var user = db.Users.FirstOrDefault(usr => usr.UserId == id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return  RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Models.User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var user = db.Users.FirstOrDefault(x => x.UserId == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Update(Models.User updatedUser)
        {
            var user = db.Users.FirstOrDefault(x => x.UserId == updatedUser.UserId);
            user.UserName = updatedUser.UserName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
