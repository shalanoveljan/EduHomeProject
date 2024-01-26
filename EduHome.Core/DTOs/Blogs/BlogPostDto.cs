using EduHome.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record BlogPostDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile? ImageFile { get; set; } 
        public int AuthorId { get; set; }
        public int  CategoryIdOfBlog { get; set; }
        public List<int> TagsIdsOfBlog { get; set; } = null!;


    }
}
