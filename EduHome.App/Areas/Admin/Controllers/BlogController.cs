using EduHome.Core.DTOs;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Areas.Admin.Controllers
{
        [Area("Admin")]
        [Authorize(Roles = "Admin,SuperAdmin")]
    public class BlogController : Controller
    {
        readonly IBlogService _blogService;
        readonly IAuthorService _authorService;
        readonly ITagOfBlogService _tagService;
        readonly ICategoryOfBlogService _categoryService;
        public BlogController(IBlogService blogService, IAuthorService authorService, ITagOfBlogService tagService, ICategoryOfBlogService categoryService)
        {
            _blogService = blogService;
            _authorService = authorService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page=1)
        {

            return View(await _blogService.GetAllAsync(page));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();

                return View();
            }
            var response = await _blogService.CreateAsync(dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ModelState.AddModelError("", response.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {

            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();

            return View(await _blogService.GetAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, BlogPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();

                return View(await _blogService.GetAsync(id));
            }
            var response = await _blogService.UpdateAsync(id, dto);

            if (response.StatusCode != 200)
            {
                ViewBag.Authors = await _authorService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                ViewBag.Categories = await _categoryService.GetAllAsync();

                ModelState.AddModelError("", response.Message);
                return View(await _blogService.GetAsync(id));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _blogService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
