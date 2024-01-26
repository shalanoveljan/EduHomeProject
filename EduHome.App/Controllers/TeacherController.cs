using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHome.App.Controllers
{
    public class TeacherController : Controller
    {
        readonly ITeacherService _teacherService;
        readonly IDutyService _dutyService;
        readonly IDegreeService _degreeService;
        readonly IFacultyService _facultyService;
        readonly IHobbyService _hobbyService;
        public TeacherController(ITeacherService teacherService, IDutyService dutyService, IDegreeService degreeService, IFacultyService facultyService, IHobbyService hobbyService)
        {
            _teacherService = teacherService;
            _dutyService = dutyService;
            _degreeService = degreeService;
            _facultyService = facultyService;
            _hobbyService = hobbyService;
        }



        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.Duties = await _dutyService.GetAllAsync();
            ViewBag.Degrees = await _degreeService.GetAllAsync();
            ViewBag.Faculties = await _facultyService.GetAllAsync();
            ViewBag.Hobbies = await _hobbyService.GetAllAsync();
            return View(await _teacherService.GetAllAsync(page));
        }

        public async Task<IActionResult> Detail(int id)
        {

            return View(await _teacherService.GetAsync(id));
        }


    }
}