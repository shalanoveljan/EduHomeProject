using Microsoft.AspNetCore.Mvc;
using EduHome.Core.DTOs;
using EduHome.Service.Services.Interfaces;
using EduHome.Service.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace EduHome.App.Areas.Admin.Controllers
{
        [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

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

        public async Task<IActionResult> Index(int page=1)
        {

            return View(await _teacherService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Duties = await _dutyService.GetAllAsync();
            ViewBag.Degrees = await _degreeService.GetAllAsync();
            ViewBag.Faculties = await _facultyService.GetAllAsync();
            ViewBag.Hobbies = await _hobbyService.GetAllAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Duties = await _dutyService.GetAllAsync();
                ViewBag.Degrees = await _degreeService.GetAllAsync();
                ViewBag.Faculties = await _facultyService.GetAllAsync();
                ViewBag.Hobbies = await _hobbyService.GetAllAsync();


                return View();
            }
            var response = await _teacherService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Duties = await _dutyService.GetAllAsync();
                ViewBag.Degrees = await _degreeService.GetAllAsync();
                ViewBag.Faculties = await _facultyService.GetAllAsync();
                ViewBag.Hobbies = await _hobbyService.GetAllAsync();

                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Duties = await _dutyService.GetAllAsync();
            ViewBag.Degrees = await _degreeService.GetAllAsync();
            ViewBag.Faculties = await _facultyService.GetAllAsync();
            ViewBag.Hobbies = await _hobbyService.GetAllAsync();


            return View(await _teacherService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, TeacherPostDto dto)
        {
            //var a=ModelState.
            var c=dto;
           var a= dto.SkillKeys[0];
           var b= dto.SkillValues[0];

            if (!ModelState.IsValid)
            {
                ViewBag.Duties = await _dutyService.GetAllAsync();
                ViewBag.Degrees = await _degreeService.GetAllAsync();
                ViewBag.Faculties = await _facultyService.GetAllAsync();
                ViewBag.Hobbies = await _hobbyService.GetAllAsync();


                return View(await _teacherService.GetAsync(id));
            }
            var response = await _teacherService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Duties = await _dutyService.GetAllAsync();
                ViewBag.Degrees = await _degreeService.GetAllAsync();
                ViewBag.Faculties = await _facultyService.GetAllAsync();
                ViewBag.Hobbies = await _hobbyService.GetAllAsync();


                ModelState.AddModelError("", response.Message);
                return View(await _teacherService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _teacherService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
