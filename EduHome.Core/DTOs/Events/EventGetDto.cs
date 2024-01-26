using EduHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record EventGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string JobTime { get; set; } = null!;
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Image { get; set; } = null!;
        public CategoryOfEventGetDto Category { get; set; }
        public int CategoryIdOfEvent { get; set; }
        public IEnumerable<SpeakerGetDto> Speakers { get; set; }
        public IEnumerable<TagOfEventGetDto> TagsOfEvent { get; set; }

    }
}
