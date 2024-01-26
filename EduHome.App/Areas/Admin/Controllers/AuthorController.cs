using EduHome.Core.DTOs;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize(Roles = "Admin,SuperAdmin")]
    public class AuthorController : Controller
    {
        readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            return View(await _authorService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
              _authorService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {

            return View(await _authorService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AuthorPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(await _authorService.GetAsync(id));
            }
           await _authorService.UpdateAsync(id, dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _authorService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
