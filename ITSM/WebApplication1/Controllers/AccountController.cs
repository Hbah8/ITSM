using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AccountController : BaseController
    {

        public AccountController(TicketSystemDbContext context) : base(context)
        {

        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await this.Context.Users.Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    if(user.Role.Name == "admin")
                    {
                        return RedirectToAction("AdminPanel", "Dashboard");
                    }

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        #endregion

        #region Register

        [Authorize]
        [HttpGet]
        public IActionResult Register()
        {
            if (User.IsInRole("admin"))
            {
                return View(); 
            }
            return Content("У вас нет доступа");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (User.IsInRole("admin"))
            {
                if (ModelState.IsValid)
                {
                    User user = await this.Context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user == null)
                    {
                        this.Context.Users.Add(
                            new User
                            {
                                Email = model.Email,
                                Password = model.Password,
                                Role = await this.Context.Roles.FirstOrDefaultAsync(r => r.Name == model.Role)
                    });

                        await this.Context.SaveChangesAsync();

                        User newUser = await this.Context.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync();
                        UserProfile profile = new UserProfile()
                        {
                            Name = "new user",
                            UserId = newUser.Id
                        };

                        this.Context.Profiles.Add(profile);

                        await this.Context.SaveChangesAsync();

                        return RedirectToAction("SetProfile", "Dashboard", new { id = newUser.Id });
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Profile

        [Route("Account/Profile")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var profile = await this.UserProfile;

            return View(profile);
        }

        [Route("Account/Profile/{id}")]
        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {
            var profile = await this.Context.Profiles.Where(u => u.UserId == id).FirstOrDefaultAsync();

            return View(profile);
        }

        [Route("Account/Profile/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var profile = await this.UserProfile;

            ViewData["Имя пользователя"] = profile?.Name;
            ViewData["Возраст пользователя"] = profile?.Age;

            return View();
        }

        
        [Route("Account/Profile/Edit/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var currentUser = await this.Context.Users.Where(u => u.Email == User.Identity.Name).Include(u => u.Role).FirstOrDefaultAsync();

            if (currentUser.Role?.Name == "admin" || currentUser.Role?.Name == "specialist" || currentUser.Id == id)
            {
                var profile = await this.Context.Profiles.Where(u => u.UserId == id).FirstOrDefaultAsync();

                ViewData["Идентификатор"] = id;
                ViewData["Имя пользователя"] = profile?.Name;
                ViewData["Возраст пользователя"] = profile?.Age;

                return View();
            }

            else
                return RedirectToAction("Index", "Home");
        }

        [Route("Account/Profile/Edit/Save")]
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Save(EditProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var profile = await this.Context.Profiles.Where(u => u.UserId == model.UserId).FirstOrDefaultAsync();

                profile.Age = model.Age;
                profile.Name = model.Name;

                await this.Context.SaveChangesAsync();

                return RedirectToAction("Profile", new { id = profile.UserId });
            }
            return View();
        }

        #endregion
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
