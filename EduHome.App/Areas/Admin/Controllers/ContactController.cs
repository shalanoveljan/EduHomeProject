using EduHome.Service.Helpers;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class ContactController : Controller
    {
        readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _contactService.GetAllAsync());
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _contactService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
