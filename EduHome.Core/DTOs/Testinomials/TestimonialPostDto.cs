using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public class TestimonialPostDto
    {
        public IFormFile? Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<int> PositionsIds { get; set; }
    }
}
