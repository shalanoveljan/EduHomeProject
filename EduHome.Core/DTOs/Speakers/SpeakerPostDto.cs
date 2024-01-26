using EduHome.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record SpeakerPostDto
    {
        public string FullName { get; set; } = null!;
        public int PositionId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
