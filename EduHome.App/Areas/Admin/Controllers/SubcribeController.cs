using EduHome.Core.DTOs;
using EduHome.Service.Helpers;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class SubcribeController : Controller
    {
        readonly ISubcribeService _subcribeService;

        public SubcribeController(ISubcribeService subcribeService)
        {
            _subcribeService = subcribeService;
        }

        public IActionResult Index()
        {
            return View(_subcribeService.GetAllAsync());
        }

        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(SubcribePostDto dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    var response = await _subcribeService.CreateAsync(dto);

        //    if (response.StatusCode != 200)
        //    {
        //        ModelState.AddModelError("", response.Message);
        //        return View();
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
