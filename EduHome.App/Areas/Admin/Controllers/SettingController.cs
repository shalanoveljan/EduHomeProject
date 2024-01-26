using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class SettingController : Controller
    {
        readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            return View(await _settingService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SettingPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _settingService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _settingService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, SettingPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(await _settingService.GetAsync(id));
            }
            var response = await _settingService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View(await _settingService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _settingService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
