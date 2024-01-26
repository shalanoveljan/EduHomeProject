using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class TestimonialController : Controller
    {
        readonly ITestimonialService _testimonialService;
        readonly IPositionOfTestimonialService _positionService;

        public TestimonialController(ITestimonialService TestimonialService, IPositionOfTestimonialService positionService)
        {
            _testimonialService = TestimonialService;
            _positionService = positionService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            return View(await _testimonialService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _positionService.GetAllAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestimonialPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _positionService.GetAllAsync();


                return View();
            }
            var response = await _testimonialService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Positions = await _positionService.GetAllAsync();
               
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Positions = await _positionService.GetAllAsync();
            return View(await _testimonialService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, TestimonialPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _positionService.GetAllAsync();

                return View(await _testimonialService.GetAsync(id));
            }
            var response = await _testimonialService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Positions = await _positionService.GetAllAsync();

                ModelState.AddModelError("", response.Message);
                return View(await _testimonialService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _testimonialService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
