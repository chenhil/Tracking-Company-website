using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShippingCo.Models;
using ShippingCo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingCo.Controllers.Auth
{
    public class AuthController : Controller
    {
        private SignInManager<Users> _signInManager;

        public AuthController(SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
        }


        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

                if(signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "App");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password Incorrect.");
                }
            }
            return View();
        }

        public IActionResult login()
        {
            return View();
        }

        public async Task<IActionResult> logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }



    }
}
