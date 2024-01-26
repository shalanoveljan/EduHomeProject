using EduHome.Core.DTOs;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHome.App.Controllers
{
    public class EventController : Controller
    {
        readonly IEventService _eventService;
        readonly ISpeakerService _speakerService;
        readonly ITagOfEventService _tagService;
        readonly ICategoryOfEventService _categoryService;
        public EventController(IEventService eventService, ISpeakerService speakerService, ITagOfEventService tagService, ICategoryOfEventService categoryService)
        {
            _eventService = eventService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(await _eventService.GetAllAsync(page));
        }

        public async Task<IActionResult> Detail(int id)
        {

            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();

            return View(await _eventService.GetAsync(id));
        }

    }
}