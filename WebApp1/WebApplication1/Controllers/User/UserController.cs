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
            if(db.Users != null)
            {
                return View(db.Users.ToList());
            }
            else
            {
                throw new NullReferenceException("Users set does not exist in current TransactionContext");
            }
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
            if(user != null)
            {
                if (TryValidateModel(user))
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var user = db.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null) return View(user);
            else return RedirectToAction("Index");
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
