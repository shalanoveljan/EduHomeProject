using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record CategoryOfBlogGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int BlogCount { get; set; }

    }
}
