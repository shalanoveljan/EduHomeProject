using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class TestimonialPosition:BaseEntity
    {
        public Testimonial Testimonial { get; set; }
        public int TestimonialId { get; set; }
        public PositionOfTestimonial PositionOfTestimonial { get; set; }
        public int PositionOfTestimonialId { get; set; }
    }
}
