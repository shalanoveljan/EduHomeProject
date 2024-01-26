using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class TagOfEventController : Controller
    {
        readonly ITagOfEventService _tagService;

        public TagOfEventController(ITagOfEventService TagService)
        {
            _tagService = TagService;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            return View(await _tagService.GetAllAsync(page));
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagOfEventPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _tagService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _tagService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await _tagService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, TagOfEventPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _tagService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
