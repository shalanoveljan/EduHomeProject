using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHome.App.Controllers
{
    public class CourseController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

 
    }
}