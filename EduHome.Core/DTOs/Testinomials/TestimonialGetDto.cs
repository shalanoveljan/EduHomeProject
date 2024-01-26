using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public class TestimonialGetDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<PositionOfTestimonialGetDto> PositionOfTestimonials { get;set; }
    }
}
