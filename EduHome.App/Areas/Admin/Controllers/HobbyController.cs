using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
        [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class HobbyController : Controller
    {
        readonly IHobbyService _hobbyService;

        readonly ITeacherService _teacherService;
        public HobbyController(IHobbyService hobbyService, ITeacherService teacherService)
        {
            _hobbyService = hobbyService;
            _teacherService = teacherService;
        }


        public async Task<IActionResult> Index(int page=1)
        {

            return View(await _hobbyService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = await _teacherService.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HobbyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Teachers = await _teacherService.GetAllAsync();
                return View();
            }
            var response = await _hobbyService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Teachers = await _teacherService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Teachers = await _teacherService.GetAllAsync();

            return View(await _hobbyService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, HobbyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Teachers = await _teacherService.GetAllAsync();

                return View(await _hobbyService.GetAsync(id));
            }
            var response = await _hobbyService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Teachers = await _teacherService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View(await _hobbyService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _hobbyService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
