using EduHome.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record BlogGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int CommentCount { get; set; }
        public AuthorGetDto AuthorGetDto { get; set; }
        public DateTime Date { get; set; }
        public CategoryOfBlogGetDto Category { get; set; }
        public int CategoryIdOfBlogGetDto { get; set; }
        public IEnumerable<TagOfBlogGetDto> TagsOfBlog { get; set; } = null!;

        public List<EduHome.Core.Entities.Comment> Comments { get; set; }
        public EduHome.Core.Entities.Comment Comment { get; set; }

        public string Text { get; set; }
    }
}
