using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class AccountController : Controller
    {
       private readonly SignInManager<IdentityUser> _signInManager;
       private readonly UserManager<IdentityUser> _userManager;

       public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
       {
          _signInManager = signInManager;
          _userManager = userManager;
       }

      
       public IActionResult Login()
       {
          return View("Login");
       }

       [HttpPost]
       public async Task<IActionResult> Login(LoginViewModel loginViewModel)
       {
          if (!ModelState.IsValid)
          {
             return View(loginViewModel);
          }

          var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

          if (user != null)
          {
             var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
             if (result.Succeeded)
             {
                return RedirectToAction("Index", "Home");
             }
          }

          ModelState.AddModelError("", "Username / Password combination not found");
          return View(loginViewModel);
       }

       public IActionResult Register()
       {
          return View();
       }

       [HttpPost]
       public async Task<IActionResult> Register(LoginViewModel vm)
       {
          if (ModelState.IsValid)
          {
             var user = new IdentityUser
             {
                UserName = vm.UserName
             };

             //This create a user
             var result = await _userManager.CreateAsync(user, vm.Password);

             if (result.Succeeded)
             {
                return RedirectToAction("Index", "Home");
             }
          }

          return View(vm);
       }

       [HttpPost]
       public async Task<IActionResult> Logout()
       {
          await _signInManager.SignOutAsync();
          return RedirectToAction("Index", "Home");
       }
    }
}
