using EduHome.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record EventPostDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string JobTime { get; set; } = null!;
        public DateTime EndDate { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int CategoryIdOfEvent { get; set; }
        public List<int> SpeakersIds{ get; set; }
        public List<int> TagsIdsOfEvent { get; set; } = null!;
    }
}
