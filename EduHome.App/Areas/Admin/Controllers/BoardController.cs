using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class BoardController : Controller
    {
        readonly IBoardService _BoardService;

        public BoardController(IBoardService BoardService)
        {
            _BoardService = BoardService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            return View(await _BoardService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoardPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _BoardService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _BoardService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, BoardPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(await _BoardService.GetAsync(id));
            }
            var response = await _BoardService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ModelState.AddModelError("", response.Message);
                return View(await _BoardService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _BoardService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
