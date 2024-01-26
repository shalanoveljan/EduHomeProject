using EduHome.App.ViewModels;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHome.App.Controllers
{
    public class AboutController : Controller
    {

        readonly ISettingService _settingService;

        readonly ITeacherService _teacherService;
        readonly IBoardService _boardService;
        readonly ITestimonialService _testimonialService;
        public AboutController(ISettingService settingService, ITeacherService teacherService, IBoardService boardService, ITestimonialService testimonialService)
        {
            _settingService = settingService;
            _teacherService = teacherService;
            _boardService = boardService;
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> Index()
        {
            AboutViewModel aboutvm = new AboutViewModel();

            var settingresponse = await _settingService.GetAllAsync();
            aboutvm.settings = settingresponse.Items;

            var teacherresponse = await _teacherService.GetAllAsync();
            aboutvm.teachers = teacherresponse.Items;

            var boardresponse = await _boardService.GetAllAsync();
            aboutvm.boards = boardresponse.Items;

            var testimonialresponse = await _testimonialService.GetAllAsync();
            aboutvm.testimonials = testimonialresponse.Items;

            return View(aboutvm);
            //return View();
        }

 
    }
}