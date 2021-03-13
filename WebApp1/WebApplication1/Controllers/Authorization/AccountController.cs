using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers.Authorization
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Models.Authorization.Validation.AuthorisationUserView userInfo)
        {
            if (TryValidateModel(userInfo))
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = userInfo.UserName,
                    Email = userInfo.Email
                };
                var resultOfCreating = await _userManager.CreateAsync(user, userInfo.Password);
                if (resultOfCreating.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            else
            {
                return View(userInfo);
            }
        }


    }
}
