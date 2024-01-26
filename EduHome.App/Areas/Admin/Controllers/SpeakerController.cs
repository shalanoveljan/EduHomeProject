using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class SpeakerController : Controller
    {
        readonly ISpeakerService  _speakerService;
        readonly IPositionOfSpeakerService _positionService;

        public SpeakerController(ISpeakerService speakerService, IPositionOfSpeakerService positionService)
        {
            _speakerService = speakerService;
            _positionService = positionService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            return View(await _speakerService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()     
        {
            ViewBag.Positions = await _positionService.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpeakerPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _positionService.GetAllAsync();
                return View();
            }
            var response = await _speakerService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Positions = await _positionService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Positions = await _positionService.GetAllAsync();
            return View(await _speakerService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, SpeakerPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _positionService.GetAllAsync();
                return View(await _speakerService.GetAsync(id));
            }
            var response = await _speakerService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Positions = await _positionService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View(await _speakerService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _speakerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
