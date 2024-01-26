using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Testimonial:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TestimonialPosition> TestimonialPositions { get; set; }

    }
}
