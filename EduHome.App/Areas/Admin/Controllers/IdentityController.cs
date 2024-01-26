using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Service.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IdentityController : Controller
    {

        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;

        public IdentityController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment webHostEnvironment, IEmailService emailService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser appUser = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);


            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            }

            if (appUser == null)
            {
                ModelState.AddModelError("", "Username or email or password incorrect");
                return View();
            }

            var roles = await _userManager.GetRolesAsync(appUser);

            if (!roles.Contains("Admin") && !roles.Contains("SuperAdmin"))
            {
                ModelState.AddModelError("", "Username or email or password incorrect");
                return View();
            }


            Microsoft.AspNetCore.Identity.SignInResult res = await _signInManager.PasswordSignInAsync(appUser, loginDto.Password, true, false);
            if (!res.Succeeded)
            {
                if (res.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your Account was blocked for 1 minutes");
                    return View();
                }

                ModelState.AddModelError("", "Username or email or password incorrect");
                return View();
            }


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }












        //public async Task<IActionResult> Index()
        //{
        //    return Json(_roleManager.Roles.ToList());
        //}

        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    return Json("ok");
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser { Email = "Admin@eduhome.com", Name = "Eljan", Surname = "Salanov", UserName = "Elcan" };
        //    IdentityResult res = await _userManager.CreateAsync(user, "Elcan123@");

        //    if (!res.Succeeded)
        //    {
        //        return Json(res.Errors);
        //    }

        //    await _userManager.AddToRoleAsync(user, "SuperAdmin");
        //    return Json("ok");
        //}

    }
}
