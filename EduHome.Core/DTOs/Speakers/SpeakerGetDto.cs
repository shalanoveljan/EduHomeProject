using EduHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record SpeakerGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public PositionGetDto Position { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
