using EduHome.Core.DTOs;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHome.App.Controllers
{
    public class ContactController : Controller
    {

        readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactPostDto dto)
        {
            if(!ModelState.IsValid)
            {
                return View(nameof(Index));
            }
            await _contactService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}