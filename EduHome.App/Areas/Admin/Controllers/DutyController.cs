using EduHome.Core.DTOs;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
        [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DutyController : Controller
    {
       
        readonly IDutyService _dutyService;

        public DutyController(IDutyService dutyService)
        {
            _dutyService = dutyService;
        }

        public async Task<IActionResult> Index(int page=1)
        {

            return View(await _dutyService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DutyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _dutyService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _dutyService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, DutyPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(await _dutyService.GetAsync(id));
            }
            var response = await _dutyService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View(await _dutyService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _dutyService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
