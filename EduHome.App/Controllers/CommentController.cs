using EduHome.Core.DTOs.Comment;
using EduHome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EduHome.App.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        readonly IBlogService _blogService;

        public CommentController(ICommentService commentService, IBlogService blogService)
        {
            _commentService = commentService;
            _blogService = blogService;
        }
      
    }
}
