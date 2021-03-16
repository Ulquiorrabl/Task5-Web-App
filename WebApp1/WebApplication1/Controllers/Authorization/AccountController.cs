using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models.Authorization;

namespace WebApplication1.Controllers.Authorization
{
    public class AccountController : Controller
    {
        private readonly UserManager<AuthorizationUser> _userManager;
        private readonly SignInManager<AuthorizationUser> _signInManager;

        public AccountController(UserManager<AuthorizationUser> userManager, SignInManager<AuthorizationUser> signInManager)
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
            Console.WriteLine(TryValidateModel(userInfo));
            if (TryValidateModel(userInfo))
            {
                var user = new AuthorizationUser
                {
                    UserName = userInfo.UserName,
                    Email = userInfo.Email
                };
                var resultOfCreating = await _userManager.CreateAsync(user, userInfo.Password);
                if (resultOfCreating.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var valError in resultOfCreating.Errors)
                    {
                        ModelState.AddModelError(string.Empty, valError.Description);
                    }
                    return View(userInfo);
                }

            }
            else
            {
                return View(userInfo);
            }
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Models.Authorization.Validation.AuthorisationLoginView userLogin)
        {
            if(_userManager != null)
            {
                if (userLogin != null && TryValidateModel(userLogin))
                {
                    var user = await _userManager.FindByEmailAsync(userLogin.Email);
                    if (user != null)
                    {
                        var passwordValidation = await _userManager.CheckPasswordAsync(user, userLogin.Password);
                        if (passwordValidation)
                        {
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Incorrect username or password");
                return View();
            }
            else
            {
                throw new NullReferenceException("Identity User Manager is not exist in current services");
            }

        }
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
