using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Index()
        {
            return View();  
        }
    }
}
