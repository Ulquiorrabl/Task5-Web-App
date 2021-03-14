using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers.User
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        Models.Authorization.AuthorizationContext db;
        UserManager<Models.Authorization.AuthorizationUser> _userManager;
        public UserController(Models.Authorization.AuthorizationContext db, UserManager<Models.Authorization.AuthorizationUser> userManager)
        {
            this.db = db;
            this._userManager = userManager;
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

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
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
        public IActionResult Create(Models.Authorization.AuthorizationUser user)
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
        public IActionResult Update(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user != null) return View(user);
            else return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(Models.Authorization.AuthorizationUser updatedUser)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == updatedUser.Id);
            user.UserName = updatedUser.UserName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
