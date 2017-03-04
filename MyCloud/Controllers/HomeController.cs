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

        public HomeController(SignInManager<CloudUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            /*if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                if (signInResult.Succeeded)
                {
                    
                }
                else
                {
                    return BadRequest();
                }
            }

            return Accepted();*/
            return RedirectToAction("MainPanel", "Logged");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            /*if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                if (signInResult.Succeeded)
                {
                    
                }
                else
                {
                    return BadRequest();
                }
            }

            return Accepted();*/
            return RedirectToAction("MainPanel", "Logged");
        }
    }
}
