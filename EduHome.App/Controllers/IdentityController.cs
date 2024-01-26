﻿using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using EduHome.Service.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Controllers
{
    public class IdentityController : Controller
    {

        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IEmailService _emailService;
        readonly IWebHostEnvironment _webHostEnvironment;
        public IdentityController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IWebHostEnvironment webHostEnvironment)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new AppUser
            {
                Email = dto.Email,
                UserName = dto.Username,
                Name = dto.Name,
                Surname = dto.Surname
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(appUser, "User");


            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var url = $"{Request.Scheme}://{Request.Host}{Url.Action("VerifyEmail", "Identity", new { email = appUser.Email, token = token })}";

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Templates", "Verify.html");

            string body = string.Empty;

            body = System.IO.File.ReadAllText(path);

            body = body.Replace("{{url}}", url);


            await _emailService.SendEmail(appUser.Email, "Verify Email", body);
            TempData["emailverify"] = "verify";
            return RedirectToAction("index", "Home");
        }


        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            var res = await _userManager.ConfirmEmailAsync(appUser, token);
            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View() ;
            }

            return RedirectToAction(nameof(Login), "Identity");
        }   

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = await _userManager.FindByNameAsync(dto.UserNameOrEmail);

            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);
            }

            if (appUser == null)
            {
                ModelState.AddModelError("", "Username or email or password incorrect");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult res = await _signInManager.PasswordSignInAsync(appUser, dto.Password, dto.RememberMe, true);
            if (!res.Succeeded)
            {
                if (res.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your Account was blocked for 1 minutes");
                    return View();
                }

                if (res.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Verify your email");
                    return View();
                }

                ModelState.AddModelError("", "Username or email or password incorrect");
                TempData["login"] = "login";

                return View();
            }


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["logout"] = "logout";

            return RedirectToAction("index", "home");
        }
        [Authorize]
        public async Task<IActionResult> Update()
        {
            var query = _userManager.Users.Where(x => x.UserName == User.Identity.Name);
            UpdateDto? updateDto = await query.Select(x => new UpdateDto { UserName = x.UserName, Name = x.Name, Surname = x.Surname })
                .FirstOrDefaultAsync();
            return View(updateDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(UpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            appUser.Name = dto.Name;
            appUser.Surname = dto.Surname;
            appUser.UserName = dto.UserName;
            IdentityResult result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            if (!string.IsNullOrWhiteSpace(dto.OldPassword))
            {
                if (dto.NewPassword == null)
                {
                    dto.NewPassword = dto.OldPassword;
                }

                var res = await _userManager.ChangePasswordAsync(appUser, dto.OldPassword, dto.NewPassword);

                if (!res.Succeeded)
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View();
                }
            }
            await _signInManager.SignInAsync(appUser, true);
            TempData["update"] = "update";

            return RedirectToAction(nameof(Update));
        }


        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);


            if (appUser == null)
            {
                ModelState.AddModelError("", "please add valid email");
                return View();
            }


            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            var url = $"{Request.Scheme}://{Request.Host}{Url.Action("ResetPassword", "Identity", new { email = appUser.Email,  token })}";

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Templates", "Verify.html");

            string body = string.Empty;

            body = System.IO.File.ReadAllText(path);

            body = body.Replace("{{url}}", url);


            await _emailService.SendEmail(appUser.Email, "Reset Passsword", body);
            TempData["resetPassword"] = "reset";
            return RedirectToAction("index", "home");
        }


        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);

            if (appUser == null)
            {
                return NotFound();
            }
            return View(new ResetPasswordDto { Email = email, Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(dto.Email);

            if (appUser == null)
            {
                return NotFound();
            }

            var res = await _userManager.ResetPasswordAsync(appUser, dto.Token, dto.Password);

            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(dto);   
            }
            return RedirectToAction(nameof(Login));
        }

    }
}