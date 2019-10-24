﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TryAuthorization.Models;
using TryAuthorization.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace TryAuthorization.Controllers
{
    public class AccountController: Controller
    {
        private UserContext db;
        public AccountController(UserContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  // google it
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);//maaaagic
                if (user != null)
                {
                    await Authenticate(model.Email);
                    return RedirectToAction("Index", "Home");

                }
                else ModelState.AddModelError("", "Некорректные логин и/или пароль");

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    db.Users.Add(new User { Email = model.Email, Password = model.Password });
                    await db.SaveChangesAsync();
                    await Authenticate(model.Email);
                    return RedirectToAction("Index", "Home");

                }
                else
                    ModelState.AddModelError("", "Некорректный логин или пароль");
            }
            return View(model);
            


        }
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
              new Claim(ClaimsIdentity.DefaultNameClaimType,userName)
            };
            ClaimsIdentity Id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Id));

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }

}