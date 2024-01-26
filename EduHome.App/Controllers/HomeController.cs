using EduHome.App.ViewModels;
using EduHome.Core.Entities;
using EduHome.Data.Contexts;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EduHome.App.Controllers
{
    public class HomeController : Controller
    {
        readonly ISliderService _sliderService;
        readonly ISettingService _settingService;
        readonly ISubcribeService _subcribeService;
        readonly IEventService _eventService;
        readonly ITeacherService _teacherService;
        readonly IBlogService _blogService;
        readonly IBoardService _boardService;
        readonly ITestimonialService _testimonialService;
        readonly EduHomeDbContext _context;
        public HomeController(ISliderService sliderService, ISettingService settingService, IEventService eventService, ITeacherService teacherService, IBlogService blogService, IBoardService boardService, ITestimonialService testimonialService, ISubcribeService subcribeService, EduHomeDbContext context)
        {
            _sliderService = sliderService;
            _settingService = settingService;
            _eventService = eventService;
            _teacherService = teacherService;
            _blogService = blogService;
            _boardService = boardService;
            _testimonialService = testimonialService;
            _subcribeService = subcribeService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeVM = new HomeViewModel();


            var sliderResponse = await _sliderService.GetAllAsync();
            homeVM.sliders = sliderResponse.Items;

            var settingResponse = await _settingService.GetAllAsync();
            homeVM.settings = settingResponse.Items;

            var teacherResponse = await _teacherService.GetAllAsync();
            homeVM.teachers = teacherResponse.Items;

            var blogResponse = await _blogService.GetAllAsync();
            homeVM.blogs = blogResponse.Items;

            var boardResponse = await _boardService.GetAllAsync();
            homeVM.boards = boardResponse.Items;

            var eventResponse = await _eventService.GetAllAsync();
            homeVM.events = eventResponse.Items;

            var testimonialresponse = await _testimonialService.GetAllAsync();
            homeVM.testimonials = testimonialresponse.Items;

            return View(homeVM);

        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string email)
        {

            if (email == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (await _context.Subcribes.Where(x => !x.IsDeleted).AnyAsync(x => x.email == email))
            {
                TempData["noSendEmail"] = " email is already subscribed";
                return RedirectToAction("Index", "Home");
            }
            _context.Subcribes?.AddAsync(new Subcribe { email = email, CreatedAt = DateTime.Now });
            _context.SaveChangesAsync();
            TempData["SendEmail"] = "Subcribed succesfully";
            return RedirectToAction("Index", "Home");

        }

    }
}