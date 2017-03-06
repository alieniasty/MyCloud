using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Models;
using MyCloud.ViewModel;

namespace MyCloud.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        private UserManager<CloudUser> _userManager;
        private SignInManager<CloudUser> _signInManager;

        public AppController(UserManager<CloudUser> userManager, SignInManager<CloudUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Panel()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);

                var userFields = Mapper.Map<CloudUserViewModel>(user);

                return View(userFields);
            }

            return RedirectToAction("Login", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Login", "Home");
        }
    }
}
