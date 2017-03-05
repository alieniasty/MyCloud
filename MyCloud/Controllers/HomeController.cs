using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Models;
using MyCloud.ViewModel;

namespace MyCloud.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<CloudUser> _signInManager;
        private UserManager<CloudUser> _userManager;

        public HomeController(SignInManager<CloudUser> signInManager, UserManager<CloudUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Panel", "App");
            }
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Panel", "App");
                }

                ModelState.AddModelError(String.Empty, "Password or username is invalid.");
                return View();
            }

            return View();
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Panel", "App");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(vm.Email) == null)
                {
                    var user = new CloudUser()
                    {
                        UserName = vm.Username,
                        Email = vm.Email,
                        Registered = DateTime.Now
                    };

                    var registerResult = await _userManager.CreateAsync(user, vm.Password);

                    if (registerResult.Succeeded)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Username, password, confirmation pass or email is incorrect");
            return View();
        }
    }
}
