using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
        [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class EventController : Controller
    {
        readonly IEventService _eventService;
        readonly ISpeakerService _speakerService;
        readonly ITagOfEventService _tagService;
        readonly ICategoryOfEventService _categoryService;
        public EventController(IEventService eventService, ISpeakerService speakerService, ITagOfEventService tagService, ICategoryOfEventService categoryService)
        {
            _eventService = eventService;
            _speakerService = speakerService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            return View(await _eventService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Speakers = await _speakerService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Speakers = await _speakerService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();

                return View();
            }
            var response = await _eventService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Speakers = await _speakerService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync(); 
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Speakers = await _speakerService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();

            return View(await _eventService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, EventPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Speakers = await _speakerService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();

                return View(await _eventService.GetAsync(id));
            }
            var response = await _eventService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Speakers = await _speakerService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();

                ModelState.AddModelError("", response.Message);
                return View(await _eventService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _eventService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
