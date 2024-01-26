using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public int CommentCount { get; set; }
        public int CategoryIdOfBlog { get; set; }
        
        public CategoryOfBlog CategoryOfBlog { get; set; }

        public List<TagBlog> TagsBlog { get; set; }
        public List<Comment> Comments { get; set; }
        public string Storage { get; set; } = null!;

    }
}
