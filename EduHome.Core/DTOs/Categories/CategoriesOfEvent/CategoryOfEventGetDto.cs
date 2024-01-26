using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record CategoryOfEventGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int EventCount { get; set; }

    }
}
