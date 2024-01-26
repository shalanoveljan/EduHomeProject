using EduHome.Core.DTOs;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryOfBlogController : Controller
    {
        readonly ICategoryOfBlogService _categoryService;

        public CategoryOfBlogController(ICategoryOfBlogService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            return View(await _categoryService.GetAllAsync(page));
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryOfBlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _categoryService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _categoryService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _categoryService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CategoryOfBlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _categoryService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

    }
}
