using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
        [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class FacultyController : Controller
    {
       
        readonly IFacultyService _FacultyService;

        public FacultyController(IFacultyService FacultyService)
        {
            _FacultyService = FacultyService;
        }

        public async Task<IActionResult> Index(int page=1)
        {

            return View(await _FacultyService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacultyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _FacultyService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _FacultyService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, FacultyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(await _FacultyService.GetAsync(id));
            }
            var response = await _FacultyService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View(await _FacultyService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _FacultyService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
