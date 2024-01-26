using EduHome.Core.DTOs;
using EduHome.Core.DTOs.Comment;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHome.App.Controllers
{
    public class BlogController : Controller
    {
        readonly IBlogService _blogService;
        readonly IAuthorService _authorService;
        readonly ITagOfBlogService _tagService;
        readonly ICategoryOfBlogService _categoryService;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentService _commentService;

        public BlogController(IBlogService blogService, IAuthorService authorService,
            ITagOfBlogService tagService, ICategoryOfBlogService categoryService,
            ICommentRepository commentRepository, ICommentService commentService)
        {
            _blogService = blogService;
            _authorService = authorService;
            _tagService = tagService;
            _categoryService = categoryService;
            _commentRepository = commentRepository;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(string searchText,int page = 1)
        {
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();
            //List<Blog> blogs = _blogService.GetBlogsBySearchTextAsync(searchText);
            return View(await _blogService.GetBlogsBySearchTextAsync(searchText,page));
        }

        public async Task<IActionResult> Detail(int id)
        {

            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            ViewBag.Categories = await _categoryService.GetAllAsync();

            return View(await _blogService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentPostDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(dto);

            var comment = _commentService.CreateAsync(dto, id);

            return Redirect($"~/Blog/detail/{id}");
        }
    }
}