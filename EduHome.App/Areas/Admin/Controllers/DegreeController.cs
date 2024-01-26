using EduHome.Core.DTOs;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize(Roles = "Admin,SuperAdmin")]
    public class DegreeController : Controller
    {
       
        readonly IDegreeService _DegreeService;

        public DegreeController(IDegreeService DegreeService)
        {
            _DegreeService = DegreeService;
        }

        public async Task<IActionResult> Index(int page=1)
        {

            return View(await _DegreeService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DegreePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _DegreeService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _DegreeService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, DegreePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(await _DegreeService.GetAsync(id));
            }
            var response = await _DegreeService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View(await _DegreeService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _DegreeService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
