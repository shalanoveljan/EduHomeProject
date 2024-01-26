using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public class BoardGetDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public  DateTime CreatedAt { get; set; }

    }
}
