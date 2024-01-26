using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class PositionOfSpeakerController : Controller
    {

        readonly IPositionOfSpeakerService _positionService;

        public PositionOfSpeakerController(IPositionOfSpeakerService PositionService)
        {
            _positionService = PositionService;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            return View(await _positionService.GetAllAsync(page));
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PositionPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _positionService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _positionService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _positionService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PositionPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _positionService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
